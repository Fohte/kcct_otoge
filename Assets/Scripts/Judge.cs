using UnityEngine;
using System.Collections;

public class Judge : MonoBehaviour
{
  public double ExactTapTiming; //単位は秒
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

  void JudgeTiming()
  {
    actualTapTiming = MusicPlayController.ElapsedTime;
    double difference = System.Math.Abs(ExactTapTiming - actualTapTiming) * 1000; //単位はミリ秒。この差がどれぐらいかによって判定する(?)
    Destroy(gameObject);
  }
}
