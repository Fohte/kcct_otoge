using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicPlayController : MonoBehaviour
{
  int startBar;

  void Start()
  {
    var music = GameObject.Find("music").AddComponent<Music>();
    music.DebugText = GameObject.Find("status").GetComponent<TextMesh>();
    music.Sections = new List<Music.Section>();
    var musicSection = new Music.Section(startBar = 0);
    musicSection.Name = "start";
    music.Sections.Add(musicSection);
    Music.Play(name, "start");    
  }

  void Update()
  {
  
  }

}
