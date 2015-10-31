using UnityEngine;
using System.Collections;
using Otoge.Util;

public class JudgeManager : MonoBehaviour
{
  public double ExactTapTiming = 2; //単位は秒
  double actualTapTiming; //単位は秒
  public static Judge judge = Judge.Miss;

  NoteTouchManager noteTouchManager;

  void Start()
  {
    Destroy(gameObject, 1);
  }

  void Update()
  {
    var notes = GameObject.FindGameObjectsWithTag("Note");

    foreach (var note in notes)
    {
      var parent = note.transform.parent.gameObject;
      noteTouchManager = parent.GetComponent<NoteTouchManager>();

      switch (parent.transform.tag)
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

      if (judge != Judge.Miss && !GameObject.Find(judge.ToString()))
      {
        var judgeObject = Instantiate(Resources.Load("Prefabs/" + judge.ToString())) as GameObject;
        judgeObject.transform.SetParent(parent.transform);
        Destroy(judgeObject, 1);
      }
    }
    MusicPlayController.comboAndScoreUpdate();
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
