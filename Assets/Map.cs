using System.Collections.Generic;
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
      
    }

    public void Save(Header header, Command List<Command> commands, List<Note> notes, string musicId = null, Difficulty? difficulty = null)
    {
      if (musicId == null) musicId = MusicId;
      if (difficulty == null) difficulty = Difficulty;
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