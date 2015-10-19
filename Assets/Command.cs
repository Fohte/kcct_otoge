namespace Otoge.Util
{
  public class Command
  {
    public static class Channel
    {
      public const string BPMSetter = "00";
      public const string MeasureSetter = "01";
    }

    public class BPM
    {
      public int Bar;
      public string Rhythm;
      public double Value;
    }

    public class Measure
    {
      public int Bar;
      public string Rhythm;
      public int Numer;
      public int Denom;
    }

    public const string MapPrefix = "*";
  }
}