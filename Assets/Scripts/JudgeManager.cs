using UnityEngine;
using System.Collections;
using Otoge.Util;

public class JudgeManager : MonoBehaviour
{
  public const float Perfect = 1.0f;
  public const float Great = 0.7f;
  public const float Good = 0.5f;
  public const float Bad = 0.1f;
  public const float Miss = 0.0f;

  public double ExactTapTiming = 2; //単位は秒
  double actualTapTiming; //単位は秒

  void Start ()
  {
    
  }
	
	void Update ()
  {
    
	}

  void OnMouseDown() //PCでのデバッグ用にクリックに反応するようにしているが、、実際にはタッチやフリックで反応するようにする。
  {
   if (JudgeTiming() != Judge.Miss)
    {
      Destroy(gameObject);
    }
  }

  public Judge JudgeTiming()
  {
    actualTapTiming = MusicPlayController.ElapsedTime;
    double difference = System.Math.Abs(ExactTapTiming - actualTapTiming) * 1000; //単位はミリ秒。この差がどれぐらいかによって判定する(?)
    Debug.Log(actualTapTiming);
    Debug.Log(ExactTapTiming);
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
