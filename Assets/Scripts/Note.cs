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

  public static void CreateNote(float X, float Y, double timing)
  {
    GameObject note = (GameObject)Resources.Load("Prefabs/Temporary Note");
    note.GetComponent<Judge>().exactTapTiming = timing;
    Instantiate(note, new Vector3(X, Y, 0), Quaternion.identity);
  }
}
