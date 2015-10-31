using UnityEngine;

public class Tap : MonoBehaviour
{
  GameObject note;
  int fingerId;

  void Start()
  {
    foreach (Transform child in transform)
    {
      if (child.tag == "Note")
      {
        note = child.gameObject;
      }
    }
  }

  public bool IsJustTouched()
  {
    if (Input.touchCount == 0)
      return false;
    
    for (int i = 0; i < Input.touchCount; i++)
    {
      Touch touch = Input.touches[i];
      if (touch.phase != TouchPhase.Began)
        continue;
      
      var position = Camera.main.ScreenToWorldPoint(touch.position);
      var colliders = Physics2D.OverlapPointAll(position);

      foreach (var collider in colliders)
      {
        if (note.GetInstanceID() == collider.gameObject.GetInstanceID())
          return true;
      }
    }

    return false;
  }
}
