using UnityEngine;
using System.Collections;
using Otoge.Util;

public class NoteManager : MonoBehaviour
{
  static int noteNumber = 0;
  Map map = new Map("1", Difficulty.Hard); //デバッグ用に1にしているが実際はMusicSelectController.MusicId
  static GameObject note;

  void Start()
  {
    
  }

  void Update()
  {

  }

  public static void CreateNote(float X, float Y, double timing, string kind)
  {
    switch (kind)
    {
      case "10":
        note = (GameObject)Resources.Load("Prefabs/TapObject");
        break;
      case "30":
        note = (GameObject)Resources.Load("Prefabs/FreeFlickObject");
        break;
      case "31":
        note = (GameObject)Resources.Load("Prefabs/FlickObject");
        break;
      case "32":
        note = (GameObject)Resources.Load("Prefabs/FlickObject");
        break;
      case "33":
        note = (GameObject)Resources.Load("Prefabs/FlickObject");
        break;
      case "34":
        note = (GameObject)Resources.Load("Prefabs/FlickObject");
        break;
      case "35":
        note = (GameObject)Resources.Load("Prefabs/FlickObject");
        break;
      case "36":
        note = (GameObject)Resources.Load("Prefabs/FlickObject");
        break;
      case "37":
        note = (GameObject)Resources.Load("Prefabs/FlickObject");
        break;
      case "38":
        note = (GameObject)Resources.Load("Prefabs/FlickObject");
        break;
    }

    note.GetComponent<JudgeManager>().ExactTapTiming = timing;
    note.name = "note" + noteNumber;
    X = -125 + X * 100;
    Y = -125 + Y * 100;
    var createdNote = Instantiate(note, new Vector3(X, Y, 0), Quaternion.identity) as GameObject;
    createdNote.transform.SetParent(GameObject.Find("Notes").transform,false);
    noteNumber++;
  }
}
