using StoredObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Attributes;

namespace Services
{
  public interface IQuranService
  {
    void LoadQuranFromFile();
  }
  public class QuranService : IQuranService
  {
    public void LoadQuranFromFile()
    {
      var fileName = @"C:\Users\Lamaan\Downloads\quranic-corpus-morphology-0.4 (1)\quranic-corpus-morphology-0.4.txt";
      var lines = File.ReadAllLines(fileName);
      lines.ToList().ToList().ForEach(l => ProcessWordPart(l));
    }

    private StorageService StorageService;
    private void ProcessWordPart(string line)
    {
      using (StorageService = new StorageService())
      {
        if (string.IsNullOrEmpty(line)) { return; }
        if (line[0] != '(') { return; }
        var lineParts = line.Split('\t');

        var key = lineParts[0].Trim(')', '(');
        Console.WriteLine(key);
        var keyParts = key.Split(':');
        var surahNumber = int.Parse(keyParts[0]);
        var verseNumber = int.Parse(keyParts[1]);
        var wordNumber = int.Parse(keyParts[2]);
        var wordPartNumber = int.Parse(keyParts[3]);
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
          wordPart = wordParts.Create();
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
        }
      }
    }

    private WordPartPositionType EnsureWordPartPositionTypeExists(string position)
    {
      var positionTypes = StorageService.SetOf<WordPartPositionType>();
      var wordpartpositionType = positionTypes.SingleOrDefault(p => p.Code == position);
      if (wordpartpositionType == null)
      {
        wordpartpositionType = positionTypes.Create();
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
        word = words.Create();
        word.SurahNumber = surahNumber;
        word.VerseNumber = verseNumber;
        word.WordNumber = wordNumber;
        words.Add(word);
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
        verse = verses.Create();
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
        surah = surahs.Create();
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
        root = roots.Create();
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
        prefix = prefixes.Create();
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
        wordpartType = types.Create();
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
        wordpartType = types.Create();
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
