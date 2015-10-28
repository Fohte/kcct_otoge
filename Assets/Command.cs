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

    public const char MapPrefix = '*';

    public List<BPM> BPMs = new List<BPM>();
    public List<Measure> Measures = new List<Measure>();

    public override string ToString()
    {
      string str = "";

      foreach (var bpm in BPMs)
      {
        str += MapPrefix + bpm.Bar.ToString("d3") + Channel.BPMSetter + ":" + bpm.Value.ToString("f2") + ":" + bpm.Rhythm + "\n";
      }

      foreach (var measure in Measures)
      {
        str += MapPrefix + measure.Bar.ToString("d3") + Channel.MeasureSetter + ":" + measure.Numer.ToString("d") + "/" + measure.Denom.ToString("d") + ":" + measure.Rhythm + "\n";
      }

      return str;
    }
  }
}
