using DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
  public interface IQuranService
  {
    WordPart LoadQuranPartsFromFile(int maxPartsToAddInbatch, WordPart startingPoint);
  }
  public class QuranService : IQuranService
  {
    public IStorageService StorageService { get; }

    public QuranService(IStorageService storageService)
    {
      StorageService = storageService;
    }
    public WordPart LoadQuranPartsFromFile(int maxWordParts, WordPart startingPoint)
    {
      //StorageService.NoTracking();
      var fileName = @"C:\Users\lamaa\Documents\AAAWork\2015laptopdownloads\quranic-corpus-morphology-0.4 (1)\quranic-corpus-morphology-0.4.txt";
      //var fileName = @"C:\Users\Lamaan\Downloads\quranic-corpus-morphology-0.4 (1)\quranic-corpus-morphology-0.4.txt";
      var count = 0;
      var lines = File.ReadAllLines(fileName);
      WordPart lastWordPart = null;
      foreach (var line in lines) {

        var wordPart = ProcessWordPart(line,startingPoint);
        if (wordPart != null)
        {
          lastWordPart = wordPart;
          count++;
          if (count >= maxWordParts)
          {
            return wordPart;
          }
        }
      }
      return lastWordPart;
    }

    private WordPart ProcessWordPart(string line, WordPart startingPoint)
    {
      if (string.IsNullOrEmpty(line)) { return null; }
      if (line[0] != '(') { return null; }
      var lineParts = line.Split('\t');

      var key = lineParts[0].Trim(')', '(');
      Console.WriteLine(key);
      var keyParts = key.Split(':');
      var surahNumber = int.Parse(keyParts[0]);
      var verseNumber = int.Parse(keyParts[1]);
      var wordNumber = int.Parse(keyParts[2]);
      var wordPartNumber = int.Parse(keyParts[3]);

      if (startingPoint != null)
      {
        if (startingPoint.SurahNumber > surahNumber)
        {
          return null;
        }
        if (startingPoint.SurahNumber == surahNumber && startingPoint.VerseNumber > verseNumber)
        {
          return null;
        }
        if (startingPoint.SurahNumber == surahNumber && startingPoint.VerseNumber == verseNumber
          && startingPoint.WordNumber > wordNumber)
        {
          return null;
        }
        if (startingPoint.SurahNumber == surahNumber && startingPoint.VerseNumber == verseNumber
         && startingPoint.WordNumber == wordNumber && startingPoint.WordPartNumber > wordPartNumber)
        {
          return null;
        }
      }
      var surah = EnsureSurahExists(surahNumber);
      var verse = EnsureVerseExists(verseNumber, surahNumber);
      var word = EnsureWordExists(wordNumber, verseNumber, surahNumber);
      var form = lineParts[1];
      var tag = lineParts[2];
      var features = lineParts[3];
      var featuresList = features.Split('|');
      var position = featuresList[0];
      var wordPartForm = EnsureWordPartFormExists(form);
      var wordPartType = EnsureWordPartTypeExists(tag);
      var wordPartPositionType = EnsureWordPartPositionTypeExists(position);

      var wordParts = StorageService.SetOf<WordPart>();
      var wordPart = wordParts.SingleOrDefault(w => w.SurahNumber == surahNumber && w.VerseNumber == verseNumber && w.WordNumber == wordNumber && w.WordPartNumber == wordPartNumber);
      if (wordPart == null)
      {
        wordPart = new WordPart();
        wordPart.SurahNumber = surahNumber;
        wordPart.VerseNumber = verseNumber;
        wordPart.WordNumber = wordNumber;
        wordPart.WordPartNumber = wordPartNumber;
        wordPart.Text = wordPartForm.Text;
        wordPart.WordPartTypeCode = wordPartType.Code;
        wordPart.WordPartPositionTypeCode = wordPartPositionType.Code;
        wordParts.Add(wordPart);
        StorageService.SaveChanges();
        AddFeatures(wordPart, features);
        StorageService.DetachAllEntities();
        return wordPart;
      }
      return null;

    }

    private WordPartPositionType EnsureWordPartPositionTypeExists(string position)
    {
      var positionTypes = StorageService.SetOf<WordPartPositionType>();
      var wordpartpositionType = positionTypes.SingleOrDefault(p => p.Code == position);
      if (wordpartpositionType == null)
      {
        wordpartpositionType = new WordPartPositionType();
        wordpartpositionType.Code = position;
        positionTypes.Add(wordpartpositionType);
        StorageService.SaveChanges();
      }
      return wordpartpositionType;
    }

    private Word EnsureWordExists(int wordNumber, int verseNumber, int surahNumber)
    {
      var words = StorageService.SetOf<Word>();
      var word = words.SingleOrDefault(w => w.SurahNumber == surahNumber && w.VerseNumber==verseNumber && w.WordNumber == wordNumber);
      if (word == null)
      {
        word = new Word();
        word.SurahNumber = surahNumber;
        word.VerseNumber = verseNumber;
        word.WordNumber = wordNumber;
        word = words.Add(word).Entity;
        StorageService.SaveChanges();
      }
      return word;
    }

    private Verse EnsureVerseExists(int verseNumber, int surahNumber)
    {
      var verses = StorageService.SetOf<Verse>();
      var verse = verses.SingleOrDefault(v => v.SurahNumber == surahNumber && v.VerseNumber == verseNumber);
      if (verse == null)
      {
        verse = new Verse();
        verse.SurahNumber = surahNumber;
        verse.VerseNumber = verseNumber;
        verses.Add(verse);
        StorageService.SaveChanges();
      }
      return verse;
    }

    private Surah EnsureSurahExists(int surahNumber)
    {
      var surahs = StorageService.SetOf<Surah>();
      var surah = surahs.SingleOrDefault(s => s.SurahNumber == surahNumber);
      if (surah == null)
      {
        surah = new Surah();
        surah.SurahNumber = surahNumber;
        surah.EnglishName = surahNumber.ToString();//TODO add from other data source
        surah.ArabicName = surahNumber.ToString();//TODO add from other data source
        surahs.Add(surah);
        StorageService.SaveChanges();
      }
      return surah;
    }

    private void AddFeatures(WordPart wordPart, string features)
    {
      var featureList = features.Split('|');
      switch (featureList[0])
      {
        case "PREFIX":
          {
            var prefix = EnsurePrefixExists(featureList[1]);
            wordPart.PrefixUsage = wordPart.PrefixUsage ?? new PrefixUsage
            {
              Prefix = prefix,
              WordPart = wordPart
            };
            break;
          }
        case "STEM":
          {
            var dictionary = GetFeatureDictionary(featureList);
            //if (dictionary.ContainsKey("LEM"))
            //{
            //  var unmodifiedWordPart = EnsureUnmodifiedWordPartExists(dictionary["LEM"]);
            //  wordPart.UnmodifiedWordPartUsage = wordPart.UnmodifiedWordPartUsage ?? new UnmodifiedWordPartUsage
            //  {
            //    UnmodifiedWord = unmodifiedWordPart,
            //    WordPart = wordPart
            //  };
            //}
            if (dictionary.ContainsKey("ROOT"))
            {
              var root = EnsureRootExists(dictionary["ROOT"]);
              wordPart.RootUsage = wordPart.RootUsage ?? new RootUsage
              {
                Root = root,
                WordPart = wordPart
              };
            }
            break;
          }
      }
      StorageService.SaveChanges();
    }

    private Root EnsureRootExists(string identifier)
    {
      var arabicText = ToArabic(identifier);
      var roots = StorageService.SetOf<Root>();
      var root = roots.SingleOrDefault(p => p.Text == arabicText);
      if (root == null)
      {
        root = new Root();
        root.Text = arabicText;
        roots.Add(root);
        StorageService.SaveChanges();
      }
      return root;
    }

    //private UnmodifiedWordPart EnsureUnmodifiedWordPartExists(string identifier)
    //{
    //  var arabicText = ToArabic(identifier);

    //  var unmodifiedWordParts = StorageService.SetOf<UnmodifiedWordPart>();
    //  var unmodifiedWordPart = unmodifiedWordParts.SingleOrDefault(p => p.Text == arabicText);
    //  if (unmodifiedWordPart == null)
    //  {
    //    unmodifiedWordPart = unmodifiedWordParts.Create();
    //    unmodifiedWordPart.Text = arabicText;
    //    unmodifiedWordParts.Add(unmodifiedWordPart);
    //    StorageService.SaveChanges();
    //  }
    //  return unmodifiedWordPart;
    //}

    private Dictionary<string,string> GetFeatureDictionary(string[] featureList)
    {
      var dictionary = new Dictionary<string, string>();
      foreach (var feature in featureList)
      {
        var parts = feature.Split(':');
        var key = parts[0];
        var value = string.Empty;
        if (parts.Length > 1)
        {
          value = parts[1];
        }
        dictionary[key] = value;
      }
      return dictionary;
    }

    private Prefix EnsurePrefixExists(string prefixFeatures)
    {

      var prefixFeatureList = prefixFeatures.Split(':');
      var identifier = prefixFeatureList[0];
      var arabicText = ToArabic(identifier);

      var prefixes = StorageService.SetOf<Prefix>();
      var prefix = prefixes.SingleOrDefault(p => p.Text == arabicText);
      if (prefix == null)
      {
        prefix = new Prefix();
        prefix.Text = arabicText;
        prefixes.Add(prefix);
        StorageService.SaveChanges();
      }
      return prefix;
    }

    private WordPartType EnsureWordPartTypeExists(string tag)
    {
      var types = StorageService.SetOf<WordPartType>();
      var wordpartType = types.SingleOrDefault(p => p.Code == tag);
      if (wordpartType == null)
      {
        wordpartType = new WordPartType();
        wordpartType.Code = tag;
        types.Add(wordpartType);
        StorageService.SaveChanges();
      }
      return wordpartType;
    }

    private WordPartForm EnsureWordPartFormExists(string form)
    {
      var arabicText = ToArabic(form);

      var types = StorageService.SetOf<WordPartForm>();
      var wordpartType = types.SingleOrDefault(p => p.Text == arabicText);
      if (wordpartType == null)
      {
        wordpartType = new WordPartForm();
        wordpartType.Text = form;
        wordpartType.Text = ToArabic(form);
        types.Add(wordpartType);
        StorageService.SaveChanges();
      }
      return wordpartType;
    }

    private string ToArabic(string form)
    {
      return string.Concat(form.Select(c => QuranSearchService.Transliteration[c]));
    }
  }
}
