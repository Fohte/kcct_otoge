using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour
{
  public string Rhythm;

  void Start()
  {
   
  }

  void Update()
  {

  }

  public static void SetNoteParBeat(float X,float Y)
  {
    GameObject note = (GameObject)Resources.Load("Prefabs/Temporary Note");
    Instantiate(note, new Vector3(X, Y, 0), Quaternion.identity);
  }
}
