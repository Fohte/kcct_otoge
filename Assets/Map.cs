﻿using System.Collections.Generic;
using System.IO;
using System.Text;
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

    List<string> fileData;

    public void Load(string musicId, Difficulty difficulty)
    {
      string line;
      fileData = new List<string>();
      setFileInfo(musicId, difficulty);

      using (FileStream fs = new FileStream(MapFilePath, FileMode.Open))
      using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
      {
        while ((line = sr.ReadLine()) != null)
        {
          if (line == string.Empty) continue;
          fileData.Add(line);
        }
      } 
    }

    public void Save(Header header, List<Command> commands, List<Note> notes, string musicId, Difficulty difficulty)
    {
      setFileInfo(musicId, difficulty);
      string TestCode = "00000000";
      using (FileStream fs = new FileStream(MapFilePath, FileMode.Create))
      using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
      {
        sw.Write(TestCode);
      }
    }

    public Header ParseHeader()
    {
      var header = new Header();
      foreach (string data in fileData)
      {
        if (data[0] == Header.MapPrefix)
        {
          string key = data.Substring(1, data.IndexOf(":") - 1);
          string value = data.Substring(data.IndexOf(":") + 1);
          switch (key)
          {
            case "genre":
              header.Genre = value;
              break;
            case "title":
              header.Title = value;
              break;
            case "music_artist":
              header.MusicArtist = value;
              break;
            case "map_creator":
              header.MapCreator = value;
              break;
            case "min_bpm":
              header.MinBPM = double.Parse(value);
              break;
            case "max_bpm":
              header.MaxBPM = double.Parse(value);
              break;
            case "play_level":
              header.PlayLevel = int.Parse(value);
              break;
            case "offset":
              header.Offset = double.Parse(value);
              break;
            case "music_file":
              header.MusicFile = value;
              break;
            case "jacket_file":
              header.JacketFile = value;
              break;
            case "movie_file":
              header.MovieFile = value;
              break;
          }
        }
      }
      return header;
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