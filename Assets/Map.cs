using System.Collections.Generic;
using System.IO;
using UnityEngine;

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
    public readonly string DirPath = Application.dataPath + "/maps/";
    public string MapFilePath;
    public string MusicId;
    public Difficulty Difficulty;

    public void Load(string musicId, Difficulty difficulty)
    {
      setFileInfo(musicId, difficulty);
    }

    public void Save(Header header, List<Command> commands, List<Note> notes, string musicId, Difficulty difficulty)
    {
      setFileInfo(musicId, difficulty);
    }

    public Header ParseHeader()
    {
      return null;
    }

    public List<Note> ParseNotes()
    {
      return null;
    }

    public List<Command> ParseCommands()
    {
      return null;
    }

    void setFileInfo(string musicId, Difficulty difficulty)
    {
      this.Difficulty = difficulty;
      this.MusicId = musicId;

      string subExtension = "";
      switch (Difficulty)
      {
        case Difficulty.Easy:
          subExtension = "ez";
          break;
        case Difficulty.Medium:
          subExtension = "med";
          break;
        case Difficulty.Hard:
          subExtension = "hd";
          break;
        case Difficulty.Expert:
          subExtension = "ex";
          break;
        default:
          break;
      }
      this.MapFilePath = DirPath + MusicId + "/" + MusicId + "." + subExtension + "." + Extension;

    }
  }
}