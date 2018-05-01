using StoredObjects;
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
    QuranSearchResult Search(string searchText);
    List<SurahInformationResponse> GetSurahs();
  }
  public class QuranSearchService: IQuranSearchService
  {
    public VerseResponse GetVerse(int surahNumber, int verseNumber)
    {
      var verseResponse = new VerseResponse();
      var s = new StorageService();
      var verse  = s.SetOf<Verse>().Single(v => v.SurahNumber == surahNumber && v.VerseNumber == verseNumber);
      var words = verse.Words.Select(w => BuildWord(w)).ToList();
      verseResponse.ArabicWords= words;
      verseResponse.EnglishText = verse.Translation.Text;
      s.Dispose();
      return verseResponse;
    }


    private string BuildWord(Word w)
    {
      var parts = string.Concat(w.WordParts.Select(p => p.Text));
      return parts;
    }

   

    public QuranSearchResult Search(string searchText)
    {
      var reverseTransliterateDictionary = Transliteration.ToDictionary(x => x.Value, y => y.Key);
      var words = searchText.Split(' ');

      var s = new StorageService();
      var roots = s.SetOf<Root>().Where(r => words.Any(w=>w==r.Text)).ToList();
      var resource = new QuranSearchResult();
      var rootResults = roots.SelectMany(r => BuildSearchCategories(r.RootUsages.Select(u=>u.WordPart),"Root match")).ToList();
      //var exactUnmodifiedFormMatches = s.SetOf<UnmodifiedWordPart>().Where(r => words.Any(w => w == r.Text)).ToList();
      //var exactUnmodifiedResults = exactUnmodifiedFormMatches.SelectMany(e => BuildSearchCategories(e.Usages.Select(u => u.WordPart),"Form match")).ToList();
      var exactWholeFormMatches = s.SetOf<WordPart>().Where(r => words.Any(w => w == r.Text)).ToList();
      var exactResults = BuildSearchCategories(exactWholeFormMatches, "Exact match").ToList();


      resource.ResultCategories = exactResults.Union(rootResults).ToList();
      resource.SearchText = searchText;
      s.Dispose();
      return resource;
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
      var storageService = new StorageService();
      var results = storageService.SetOf<Surah>().Select(s=>new SurahInformationResponse { ArabicName=s.ArabicName,EnglishName=s.EnglishName,Number=s.SurahNumber,VerseCount=s.Verses.Count}).ToList();
      storageService.Dispose();
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
