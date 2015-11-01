using UnityEngine;
using System.Collections;
using Otoge.Util;

public class JudgeManager : MonoBehaviour
{
  public const float TouchLimit = 1.0f;

  public double timeFromNoteApppered;
  public double ExactTapTiming = 0; //単位は秒
  double actualTapTiming; //単位は秒
  public static Judge judge;
  // public static Judge judge = Judge.Miss;

  NoteTouchManager noteTouchManager;

  void Start()
  {
    timeFromNoteApppered = 0;
  }

  void Update()
  {
    timeFromNoteApppered += Time.deltaTime;
   
    if (GameObject.FindWithTag("Note"))
    {
      var noteObj = transform.FindChild(GameObject.FindWithTag("Note").transform.name);
      if (noteObj)
      {
        noteTouchManager = noteObj.GetComponent<NoteTouchManager>();

        if (timeFromNoteApppered >= TouchLimit)
        {
          Debug.Log("miss");
          judge = Judge.Miss;
          MusicPlayController.comboAndScoreUpdate();
          Destroy(gameObject);
        } else
        {
          switch (transform.tag)
          {
            case "Tap":
              if (!noteTouchManager.IsJustTouched())
                return;
              break;
            case "Flick":
              if (!noteTouchManager.IsFlicked())
                return;
              break;
            case "FreeFlick":
              if (!noteTouchManager.IsFlicked())
                return;
              break;
            case "Hold":
              break;
          }

          judge = JudgeTiming();

          if (!GameObject.Find(judge.ToString()))
          {
            if (judge != Judge.Miss)
            {
              Debug.Log(transform.name);
              var note = transform.FindChild(GameObject.FindWithTag("Note").transform.name);
              var approach = transform.FindChild(GameObject.FindWithTag("Approach").transform.name);
              if (note != null && approach != null)
              {
                Destroy(note.gameObject);
                Destroy(approach.gameObject);
              }
              var judgeObject = Instantiate(Resources.Load("Prefabs/" + judge.ToString()), new Vector3(0, 0, 0), transform.rotation) as GameObject;
              judgeObject.transform.SetParent(transform);
              judgeObject.transform.localPosition = Vector3.zero;
              Destroy(gameObject, 1);
            }
            MusicPlayController.comboAndScoreUpdate();
          }
        }
      }

        
    
    }

  }

  public Judge JudgeTiming()
  {
    actualTapTiming = MusicPlayController.ElapsedTime;
    double difference = System.Math.Abs(ExactTapTiming - actualTapTiming) * 1000; //単位はミリ秒。この差がどれぐらいかによって判定する(?)
    if (difference <= 42)
    {
      return Judge.Perfect;
    }
    else if (difference <= 92)
    {
      return Judge.Great;
    }
    else if (difference <= 166)
    {
      return Judge.Good;
    }
    else if (difference <= 500)
    {
      return Judge.Bad;
    }
    else
    {
      return Judge.Miss;
    }
  }
}
