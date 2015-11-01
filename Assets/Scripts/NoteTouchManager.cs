using UnityEngine;
using System;

public class NoteTouchManager : MonoBehaviour
{
  GameObject note;
  Vector3 beginPosition;
  Vector3 touchPosition;
  TouchPhase? touchState = null;
  int? fingerId;

  void Start()
  {
    /*foreach (Transform child in transform)
    {
      if (child.tag == "Note")
      {
        note = child.gameObject;
      }
    }*/
    note = gameObject;
  }

  void Update()
  {
    setTouchState();
    if (touchState == TouchPhase.Began)
    {
      beginPosition = touchPosition;
    }
  }

  void setTouchState()
  {
    if (Input.touchCount == 0)
      return;

    for (int i = 0; i < Input.touchCount; i++)
    {
      Touch touch = Input.touches[i];

      var position = Camera.main.ScreenToWorldPoint(touch.position);
      var colliders = Physics2D.OverlapPointAll(position);
      Debug.Log(position);

      foreach (var collider in colliders)
      {
        if (note.GetInstanceID() == collider.gameObject.GetInstanceID())
        {
          fingerId = touch.fingerId;
          break;
        }
      }

      if (touch.fingerId == fingerId)
      {
        touchState = touch.phase;
        touchPosition = touch.position;
        if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
          fingerId = null;
      }
    }
  }

  public bool IsJustTouched()
  {
    return touchState == TouchPhase.Began;
  }

  public bool IsTouching()
  {
    return touchState == TouchPhase.Stationary || touchState == TouchPhase.Moved;
  }

  public bool IsJustFinished()
  {
    return touchState == TouchPhase.Ended;
  }

  public bool IsFlicked()
  {
    if (!IsTouching())
      return false;

    float flickDistance = Vector3.Distance(beginPosition, touchPosition);
    Vector3 colliderSize = Camera.main.ScreenToWorldPoint(note.GetComponent<Collider2D>().bounds.size);
    float requiredDistance = Vector3.Distance(Vector3.zero, colliderSize);

    return flickDistance > requiredDistance;
  }

  public bool IsFlicked(float direction)
  {
    if (!IsTouching())
      return false;

    Vector3 diff = touchPosition - beginPosition;
    float radian = Mathf.Atan2(diff.x, diff.y) * Mathf.Rad2Deg;
    Debug.Log(radian);

    if (Mathf.Abs(direction) == 180f)
    {
      direction = Mathf.Abs(direction);
      radian = Mathf.Abs(direction);
    }
    return IsFlicked() && (direction - 22.5f < radian && radian < direction + 22.5f);
  }

}
