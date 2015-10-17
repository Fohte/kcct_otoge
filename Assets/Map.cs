using System.Collections.Generic;

namespace Otoge.Util
{
  public class Map
  {
    public static class SubExtensions
    {
      public const string Easy = "ez";
      public const string Medium = "med";
      public const string Hard = "hd";
      public const string Expert = "ex";
    }

    public const string Extension = "otm";
    public const string DirPath = "";
    public string MapDirPath;
    public string MapFilePath;

    public void Load(string musicId, Difficulty difficulty)
    {

    }

    public void Save(List<Note> notes)
    {

    }

    public void Save(List<Note> notes, string musicId)
    {

    }

    public Header ParseHeader()
    {

    }

    public List<Note> ParseNotes()
    {

    }

    public List<Command> ParseCommands()
    {

    }
  }
}