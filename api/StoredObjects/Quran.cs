using StoredObjects.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoredObjects
{
  public class Surah
  {
    public int SurahNumber { get; set; }
    public string ArabicName { get; set; }
    public string EnglishName { get; set; }
    public virtual List<Verse> Verses { get; set; }
    public virtual List<Word> Words { get; set; }
    public virtual List<WordPart> WordParts { get; set; }
  }
  public class Verse
  {
    public int SurahNumber { get; set; }
    public virtual Surah Surah { get; set; }
    public int VerseNumber { get; set; }
    public virtual List<Word> Words { get; set; }
    public virtual List<WordPart> WordParts { get; set; }
    public virtual VerseTranslation Translation { get; set; }
  }
  public class Word
  {
    public virtual Surah Surah { get; set; }
    public int SurahNumber { get; set; }
    public virtual Verse Verse { get; set; }
    public int VerseNumber { get; set; }
    public int WordNumber { get; set; }
    public virtual List<WordPart> WordParts { get; set; }
  }
  public class WordPart
  {
    public virtual Surah Surah { get; set; }
    public int SurahNumber { get; set; }
    public virtual Verse Verse { get; set; }
    public int VerseNumber { get; set; }
    public virtual Word Word { get; set; }
    public int WordNumber { get; set; }
    public int WordPartNumber { get; set; }
    public virtual WordPartForm WordPartForm { get; set; }
    public string WordPartTypeCode { get; set; }
    public virtual WordPartType WordPartType { get; set; }
    public string WordPartPositionTypeCode { get; set; }
    public WordPartPositionType WordPartPositionType { get; set; }
    public virtual RootUsage RootUsage { get; set; }
    public virtual PrefixUsage PrefixUsage { get; set; }
    public string Text { get; set; }
  }
  public class WordPartPositionType
  {
    public string Code { get; set; }
    public virtual List<WordPart> WordParts { get; set; }
  }

  public class WordPartForm
  {
    public virtual List<WordPart> WordParts { get; set; }
    public string Text { get; set; }
  }
  public class WordPartType
  {
    public string Code { get; set; }
    public virtual List<WordPart> WordParts { get; set; }

  }
  public class RootUsage
  {
    public virtual Surah Surah { get; set; }
    public int SurahNumber { get; set; }
    public virtual Verse Verse { get; set; }
    public int VerseNumber { get; set; }
    public virtual Word Word { get; set; }
    public int WordNumber { get; set; }
    public virtual WordPart WordPart { get; set; }
    public int WordPartNumber { get; set; }
    public string RootText{ get; set; }
    public virtual Root Root { get; set; }
  }
  public class Root
  {
    public virtual List<RootUsage> RootUsages { get; set; }
    public string Text { get; set; }
  }
  public class PrefixUsage
  {
    public virtual Surah Surah { get; set; }
    public int SurahNumber { get; set; }
    public virtual Verse Verse { get; set; }
    public int VerseNumber { get; set; }
    public virtual Word Word { get; set; }
    public int WordNumber { get; set; }
    public virtual WordPart WordPart { get; set; }
    public int WordPartNumber { get; set; }
    public string Text { get; set; }
    public virtual Prefix Prefix { get; set; }
  }
  public class Prefix
  {
    public string Text { get; set; }
    public virtual List<PrefixUsage> PrefixUsages { get; set; }

  }
}
