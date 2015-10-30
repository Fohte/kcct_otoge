using UnityEngine;

public class Tap : MonoBehaviour
{
  GameObject note;
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
    if (Input.GetMouseButtonDown(0))
    {
      var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      var collider = Physics2D.OverlapPoint(position);

      return collider ? true : false;
    }
    return false;
  }
}
