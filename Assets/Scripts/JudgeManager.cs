using UnityEngine;
using System.Collections;
using Otoge.Util;

public class JudgeManager : MonoBehaviour
{
  

  public double ExactTapTiming = 2; //単位は秒
  double actualTapTiming; //単位は秒
  public static Judge judge;

  void Start ()
  {

  }
	
	void Update ()
  {
    
	}

  public void OnMouseDown() //PCでのデバッグ用にクリックに反応するようにしているが、、実際にはタッチやフリックで反応するようにする。
  {
    judge = JudgeTiming();
    Debug.Log(judge);
    if (judge != Judge.Miss)
    {
     Destroy(gameObject);
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
