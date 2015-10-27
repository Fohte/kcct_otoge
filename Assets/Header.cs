namespace Otoge.Util
{
  public class Header
  {
    public const char MapPrefix = '#';
    public string Genre;
    public string Title;
    public string MusicArtist;
    public string MapCreator;
    public double MinBPM = 0;
    public double MaxBPM = 0;
    public int PlayLevel = 0;
    public Difficulty Difficulty;
    public double Offset = 0;
    public string MusicFile;
    public string JacketFile;
    public string MovieFile;

    public override string ToString()
    {
      string str = "";

      str += MapPrefix + "min_bpm:" + MinBPM + "\n";
      str += MapPrefix + "max_bpm:" + MaxBPM + "\n";
      str += MapPrefix + "play_level:" + PlayLevel + "\n";
      str += MapPrefix + "offset:" + Offset + "\n";
      if (Genre != null)
      {
        str += MapPrefix + "genre:" + Genre + "\n";
      }
      if (Title != null)
      {
        str += MapPrefix + "title:" + Title + "\n";
      }
      if (MusicArtist != null)
      {
        str += MapPrefix + "music_srtist:" + MusicArtist + "\n";
      }
      if (MapCreator != null)
      {
        str += MapPrefix + "map_creator:" + MapCreator + "\n";
      }
      if (MusicFile != null)
      {
        str += MapPrefix + "music_file:" + MusicFile + "\n";
      }
      if (JacketFile != null)
      {
        str += MapPrefix + "jacket_file:" + JacketFile + "\n";
      }
      if (MovieFile != null)
      {
        str += MapPrefix + "movie_file:" + MovieFile + "\n";
      }

      return str;
    }
  }
}
