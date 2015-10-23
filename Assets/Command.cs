using System.Collections.Generic;

namespace Otoge.Util
{
  public class Command
  {
    public static class Channel
    {
      public const string BPMSetter = "00";
      public const string MeasureSetter = "01";
    }

    public const string MapPrefix = "*";

    public List<BPM> BPMs = new List<BPM>();
    public List<Measure> Measures = new List<Measure>();
  }
}
