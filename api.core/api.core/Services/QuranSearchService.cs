using DataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiResources;

namespace Services
{
  public interface IQuranSearchService
  {
    VerseResponse GetVerse(int surahNumber, int verseNumber);
    QuranSearchResult Search(QuranSearchRequest reques);
    List<SurahInformationResponse> GetSurahs();
  }
  public class QuranSearchService: IQuranSearchService
  {
    private IStorageService _storageService;

    public QuranSearchService(IStorageService storageService)
    {
      _storageService = storageService;
    }
    public VerseResponse GetVerse(int surahNumber, int verseNumber)
    {
      var verseResponse = new VerseResponse();
      var verse  = _storageService.SetOf<Verse>().SingleOrDefault(v => v.SurahNumber == surahNumber && v.VerseNumber == verseNumber);
      if (verse == null)
      {
        return null;
      }
      var words = verse.Words.Select(w => BuildWord(w)).ToList();
      verseResponse.ArabicWords= words;
      verseResponse.EnglishText = verse.Translation?.Text??"";
      return verseResponse;
    }


    private ArabicWordResource BuildWord(Word w)
    {
      var resource = new ArabicWordResource();
      resource.Text = string.Concat(w.WordParts.Select(p => p.Text));
      resource.Prefixes = new List<string>();
      resource.Suffixes = new List<string>();
      foreach(var part in w.WordParts)
      {
        if(part.WordPartPositionTypeCode== "PREFIX")
        {
          resource.Prefixes.Add(part.Text);
        }
        else if (part.WordPartPositionTypeCode == "SUFFIX")
        {
          resource.Suffixes.Add(part.Text);
        }
        else
        {
          resource.Stem = part.Text;
          if (part.RootUsage != null)
          {
            resource.Root = part.RootUsage.RootText;
          }
        }
      }

      return resource;
    }

   

    public QuranSearchResult Search(QuranSearchRequest request)
    {
      _storageService.suspendTracking();

      if (request.SearchInEnglish)
      {
        return SearchInEnglish(request.SearchText);
      }
      //var reverseTransliterateDictionary = Transliteration.ToDictionary(x => x.Value, y => y.Key);

      var searchText = request.SearchText;

      return SearchInArabic(searchText);
    }

    public virtual QuranSearchResult SearchInArabic(string searchText)
    {
      var words = searchText.Split(' ');
      var resource = new QuranSearchResult();
      resource.ResultCategories = new List<SearchResultCategory>();
      var rootMatches = GetRootMatches(words);
      if (rootMatches.Results.Any()) {
        resource.ResultCategories.Add(rootMatches);
      }
      resource.ResultCategories.AddRange(GetRootMatchesByForm(words));

      //var roots = s.SetOf<Root>().Where(r => words.Any(w => w == r.Text)).ToList();
      //var rootResults = roots.SelectMany(r => BuildSearchCategories(r.RootUsages.Select(u => u.WordPart), "Root match")).ToList();
      //var exactUnmodifiedFormMatches = s.SetOf<UnmodifiedWordPart>().Where(r => words.Any(w => w == r.Text)).ToList();
      //var exactUnmodifiedResults = exactUnmodifiedFormMatches.SelectMany(e => BuildSearchCategories(e.Usages.Select(u => u.WordPart),"Form match")).ToList();
      //var exactWholeFormMatches = s.SetOf<WordPart>().Where(r => words.Any(w => w == r.Text)).ToList();
      //var exactResults = BuildSearchCategories(exactWholeFormMatches, "Exact match").ToList();

      //resource.ResultCategories = exactResults.Union(rootResults).ToList();
      resource.SearchText = searchText;
      return resource;
    }

    public virtual  SearchResultCategory GetRootMatches(string[] words)
    {
      IEnumerable<string> verses = null;
      foreach (var word in words)
      {
        var root = _storageService.SetOf<Root>().SingleOrDefault(r => r.Text == word);
        if (root != null)
        {
          var matches = root.RootUsages.Select(u => u.SurahNumber.ToString("000") + ":" + u.VerseNumber.ToString("000")).ToList();
          if (verses == null)
          {
            verses = matches;
          }
          else
          {
            verses = verses.Intersect(matches);
          }
        }
      }
      verses = verses ?? new List<string>();
      verses = verses.Distinct();
      var verseList = verses.ToList();
      verseList.Sort();
      var verseReferences = verseList.Select(v => new { surahNumber = int.Parse(v.Substring(0, 3)), verseNumber = int.Parse(v.Substring(4, 3)) }).ToList();

      var rootMatches = new SearchResultCategory
      {
        MatchType = "matching all root verbs in verse",
        Match = $" \"\u200f{string.Join("\u200e\", \"\u200f",words)}\u200e\"",
        Results = verseReferences.Select(v => new VerseResult { SurahNumber = v.surahNumber, VerseNumber = v.verseNumber }).ToList()
      };
      return rootMatches;
    }
    public virtual List<SearchResultCategory> GetRootMatchesByForm(string[] roots)
    {
      var categories = new List<SearchResultCategory>();
      foreach (var rootText in roots)
      {
        var root = _storageService.SetOf<Root>().Include(r=>r.RootUsages)
                    .ThenInclude(u=>u.Word.WordParts)
                    .ThenInclude(w=>w.WordPartType)
                    .SingleOrDefault(r => r.Text == rootText);
        if (root != null)
        {
          var rootUsages = root.RootUsages.ToList();
          var wordUsages = rootUsages
                        .Select(u => new {
                            Usage = u,
                            FullWord = string.Concat(u.Word.WordParts.Select(p => p.Text))
                        })
                        .ToList();
          var groups = wordUsages.GroupBy(w => w.FullWord);
          foreach (var group in groups)
          {
            var category = new SearchResultCategory();
            category.MatchType = rootText;
            category.Match = $"matching root verb \"\u200f{group.Key}\u200e\"";
            category.Results = group.Select(x => new VerseResult
            {
              SurahNumber = x.Usage.SurahNumber,
              VerseNumber = x.Usage.VerseNumber,
              WordNumber = x.Usage.WordNumber,
              WordPartNumber = x.Usage.WordPartNumber
            }).OrderBy(x=>x.SurahNumber).ThenBy(x=>x.VerseNumber).ThenBy(x=>x.WordNumber).ToList();
            categories.Add(category);
          }
        }
        else
        {
          var stemText = rootText;
          var wordParts = _storageService.SetOf<WordPart>().Where(w => w.WordPartPositionTypeCode == "STEM" && w.Text == stemText).ToList();
          var wordUsages = wordParts.Select(u => new { Usage = u, FullWord = string.Concat(u.Word.WordParts.Select(p => p.Text)) }).ToList();
          var groups = wordUsages.GroupBy(w => w.FullWord);
          foreach (var group in groups)
          {
            var category = new SearchResultCategory();
            category.MatchType = stemText;
            category.Match = $"matching word \"\u200f{group.Key}\u200e\"";
            category.Results = group.Select(x => new VerseResult
            {
              SurahNumber = x.Usage.SurahNumber,
              VerseNumber = x.Usage.VerseNumber,
              WordNumber = x.Usage.WordNumber,
              WordPartNumber = x.Usage.WordPartNumber
            }).OrderBy(x => x.SurahNumber).ThenBy(x => x.VerseNumber).ThenBy(x => x.WordNumber).ToList();
            categories.Add(category);
          }
        }
      }
      categories = categories.OrderBy(c => c.MatchType).ThenBy(x => x.Match).ToList();
      return categories;
    }

    public virtual QuranSearchResult SearchInEnglish(string searchText)
    {
      var verseTranslations = _storageService.SetOf<VerseTranslation>().Where(r => r.Text.Contains(searchText)).ToList();
      var resource = new QuranSearchResult();
      resource.ResultCategories = new List<SearchResultCategory>
      {
        new SearchResultCategory
        {
          MatchType="English verse matches",
          Match = "\""+searchText+"\"",
          Results = verseTranslations.Select(v=>BuildVerseResult(v)).ToList()
        }
      };
      resource.SearchText = searchText;
      return resource;
    }

    private VerseResult BuildVerseResult(VerseTranslation v)
    {
      return new VerseResult
      {
        SurahNumber = v.SurahNumber,
        VerseNumber = v.VerseNumber,
        WordNumber = 0,
        WordPartNumber = 0,
      };
      throw new NotImplementedException();
    }

    private List<SearchResultCategory> BuildSearchCategories(IEnumerable<WordPart> wordParts,string matchType)
    {
      var groups = wordParts.GroupBy(u => u.WordPartForm.Text);
      var categories = groups.Select(g => new SearchResultCategory
      {
        MatchType=matchType,
        Match = string.Concat(g.Key),
        Results = g.Select(r => new VerseResult { SurahNumber = r.SurahNumber, VerseNumber = r.VerseNumber, WordNumber = r.WordNumber, WordPartNumber = r.WordPartNumber }).ToList()
      });
      return categories.OrderBy(x=>x.Match).ToList();
    }

    public List<SurahInformationResponse> GetSurahs()
    {
      var results = _storageService.SetOf<Surah>().Select(s=>new SurahInformationResponse { ArabicName=s.ArabicName,EnglishName=s.EnglishName,Number=s.SurahNumber,VerseCount=s.Verses.Count}).ToList();
      return results;
    }

    public static Dictionary<char, char> Transliteration = new Dictionary<char, char> {
  {'\'','\u0621'},
{'>','\u0623'},
{'&','\u0624'},
{'<','\u0625'},
{'}','\u0626'},
{'A','\u0627'},
{'b','\u0628'},
{'p','\u0629'},
{'t','\u062A'},
{'v','\u062B'},
{'j','\u062C'},
{'H','\u062D'},
{'x','\u062E'},
{'d','\u062F'},
{'*','\u0630'},
{'r','\u0631'},
{'z','\u0632'},
{'s','\u0633'},
{'$','\u0634'},
{'S','\u0635'},
{'D','\u0636'},
{'T','\u0637'},
{'Z','\u0638'},
{'E','\u0639'},
{'g','\u063A'},
{'_','\u0640'},
{'f','\u0641'},
{'q','\u0642'},
{'k','\u0643'},
{'l','\u0644'},
{'m','\u0645'},
{'n','\u0646'},
{'h','\u0647'},
{'w','\u0648'},
{'Y','\u0649'},
{'y','\u064A'},
{'F','\u064B'},
{'N','\u064C'},
{'K','\u064D'},
{'a','\u064E'},
{'u','\u064F'},
{'i','\u0650'},
{'~','\u0651'},
{'o','\u0652'},
{'^','\u0653'},
{'#','\u0654'},
{'`','\u0670'},
{'{','\u0671'},
{':','\u06DC'},
{'@','\u06DF'},
{'"','\u06E0'},
{'[','\u06E2'},
{';','\u06E3'},
{',','\u06E5'},
{'.','\u06E6'},
{'!','\u06E8'},
{'-','\u06EA'},
{'+','\u06EB'},
{'%','\u06EC'},
{']','\u06ED'},
{' ',' '},
};
  }
}
