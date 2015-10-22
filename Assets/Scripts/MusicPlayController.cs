using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicPlayController : MonoBehaviour
{
  int startBar;

  List<string> rhythms = new List<string>()
  {
    "11111100",
    "000000000111"
  };

  void Start()
  {
    Music music = GameObject.Find("music").AddComponent<Music>();
    music.DebugText = GameObject.Find("status").GetComponent<TextMesh>();
    music.Sections = new List<Music.Section>();
    var musicSection = new Music.Section(startBar: 0);
    musicSection.Name = "start";
    music.Sections.Add(musicSection);
    var musicSection2 = new Music.Section(startBar: 2);
    musicSection2.Name = "next";
    music.Sections.Add(musicSection2);
    music.OnVal();

    Music.Play(name, "start");
  }

  void Update()
  {

  }

}
