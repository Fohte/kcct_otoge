using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour
{
  public int x = 0;
  void Start()
  {

  }

  void Update()
  {
    SetNoteParBeat();
  }

  void SetNoteParBeat()
  {
    bool changeBeat = Music.IsJustChangedBeat();
    if (changeBeat)
    {
      GameObject note = (GameObject)Resources.Load("Prefabs/Temporary Note");
      Instantiate(note, new Vector3(x, 0, 0), Quaternion.identity);
      x++;
    }
  }
}
