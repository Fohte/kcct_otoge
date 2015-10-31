using UnityEngine;

public class NotesTest : MonoBehaviour
{
  void Start()
  {

  }

  void Update()
  {
    var notes = GameObject.FindGameObjectsWithTag("Note");
    
    foreach (var note in notes)
    {
      var parent = note.transform.parent.gameObject;
      var noteTouchManager = parent.GetComponent<NoteTouchManager>();
      if (noteTouchManager.IsJustTouched())
      {
        Debug.Log(parent.transform.tag);
      }

      if (noteTouchManager.IsFlicked(90))
      {
        Destroy(parent);
      }
    }
  }
}
