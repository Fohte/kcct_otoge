namespace Otoge.Util
{
  public class Note
  {
    public static class Channel
    {
      public const string SingleTap = "10";
      public const string HoldStart = "20";
      public const string HoldEnd = "21";
      public const string FreeFlick = "30";
      public const string UpFlick = "31";
      public const string UpRightFlick = "32";
      public const string RightFlick = "33";
      public const string DownRightFlick = "34";
      public const string DownFlick = "35";
      public const string DownLeftFlick = "36";
      public const string LeftFlick = "37";
      public const string UpLeftFlick = "38";
    }

    public const char MapPrefix = '=';
    public int Bar;
    public int X;
    public int Y;
    public string Rhythm;
    public string Type;

    public override string ToString()
    {
      return MapPrefix + Bar.ToString("d3")
        + Type + X.ToString("x2") + Y.ToString("x2") + ":" + Rhythm;
    }
  }
}
