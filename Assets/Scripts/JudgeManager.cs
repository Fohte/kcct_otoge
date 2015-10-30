using UnityEngine;
using System.Collections;
using Otoge.Util;

public class JudgeManager : MonoBehaviour
{
  public double exactTapTiming; //単位は秒
  double actualTapTiming; //単位は秒

  void Start ()
  {
    
  }
	
	void Update ()
  {
    
	}

  void OnMouseDown() //PCでのデバッグ用にクリックに反応するようにしているが、、実際にはタッチやフリックで反応するようにする。
  {
    JudgeTiming();
  }

  public Judge JudgeTiming()
  {
    actualTapTiming = MusicPlayController.elapsedTime;
    double difference = System.Math.Abs(exactTapTiming - actualTapTiming) * 1000; //単位はミリ秒。この差がどれぐらいかによって判定する(?)
    Destroy(gameObject);
    if (difference <= 21)
    {
      return Judge.Perfect;
    }
    else if (difference <= 42)
    {
      return Judge.Great;
    }
    else if (difference <= 84)
    {
      return Judge.Good;
    }
    else if (difference <= 250)
    {
      return Judge.Bad;
    }
    else
    {
      return Judge.Miss;
    }
  }
}
