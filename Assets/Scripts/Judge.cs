using UnityEngine;
using System.Collections;

public class Judge : MonoBehaviour {
  public double exactTapTiming;
  double actuallyTapTiming;

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
    double actuallyTapTiming = MusicPlayController.elapsedTime;
    Debug.Log("このノートが叩かれるべきタイミングは" + exactTapTiming + "秒");
    Debug.Log("実際に叩かれたのは" + actuallyTapTiming + "秒");
    Debug.Log("その差は" + System.Math.Abs(exactTapTiming - actuallyTapTiming) * 1000 + "ミリ秒");
    Destroy(gameObject);
  }
}
