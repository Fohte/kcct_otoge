using UnityEngine;
using System.Collections;

public class NoteManager : MonoBehaviour
{
  public static int noteNumber = 0;

  void Start()
  {
    
  }

  void Update()
  {

  }

  public static void CreateNote(float X, float Y, double timing)
  {
    GameObject note = (GameObject)Resources.Load("Prefabs/Temporary Note");
    note.GetComponent<JudgeManager>().ExactTapTiming = timing;
    note.name = "note" + noteNumber;
    Instantiate(note, new Vector3(X, Y, 0), Quaternion.identity);
    noteNumber++;
  }
}
