using UnityEngine;
using System.Collections;
using Otoge.Util;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

public class MusicSelectController : MonoBehaviour
{
  List<Map> maps = new List<Map>();
  int musicNumber = 0;

  void Start()
  {
    string[] directories = Directory.GetDirectories(Application.dataPath + "/maps/");
    int totalMusic = -1;

    foreach (string path in directories)
    {
      string musicId = Path.GetFileName(path);
      var map = new Map(musicId, Difficulty.Hard);
      maps.Add(map);
      totalMusic++;
    }
    MusicSelect();

    GameObject.Find("NextButton").GetComponent<Button>().onClick.AddListener(() =>
    {
      if (musicNumber == totalMusic)
      {
        musicNumber = 0;
      }
      else
      {
        musicNumber++;
      }
      MusicSelect();
    });

    GameObject.Find("PrevButton").GetComponent<Button>().onClick.AddListener(() =>
    {
      if (musicNumber == 0)
      {
        musicNumber = totalMusic;
      }
      else
      {
        musicNumber--;
      }
      MusicSelect();
    });
  }

  public void MusicSelect()
  {
    var title = GameObject.Find("Title").GetComponent<Text>();
    var level = GameObject.Find("Level").GetComponent<Text>();
    var creator = GameObject.Find("MusicCreator").GetComponent<Text>();
    const int star = 10;

    title.text = maps[musicNumber].Header.Title;
    creator.text = maps[musicNumber].Header.MusicArtist;
    Image image = GameObject.Find("Jacket").gameObject.GetComponent<Image>();
    image.sprite = Resources.Load<Sprite>("Jackets/" + maps[musicNumber].Header.JacketFile);

    level.text = "";
    for (int i = 0; i < maps[musicNumber].Header.PlayLevel; i++)
    {
      level.text += "★";
    }
    for (int j = 0; j < star - maps[musicNumber].Header.PlayLevel; j++)
    {
      level.text += "☆";
    }
  }
}
