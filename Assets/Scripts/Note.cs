using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour
{

  public int X = 0;
  public int Y = 0;
  public int Bar = 0;
  public string Rhythm;

  void Start()
  {

  }

  void Update()
  {
    SetNoteParBeat();
  }

  void SetNoteParBeat()
  {
    if (Music.IsJustChangedBeat())
    {
      GameObject note = (GameObject)Resources.Load("Prefabs/Temporary Note");
      Instantiate(note, new Vector3(X, 0, 0), Quaternion.identity);
      X++;
    }
  }
}
