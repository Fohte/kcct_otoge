using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Otoge.Util;

public class MusicPlayController : MonoBehaviour
{
  public const int UnitPerBar = 16;  //デバッグ用。実際は音符の混合と表紙の変化がないことから、unitPerBarは48、unitPerBeatは12。
  public const int UnitPerBeat = 4;

  
  public static double ElapsedTime = 0;
  int a = 0;
  int bar = 0;
  int beat = 0;
  int unit = 0;
  double mtPerSecond = 0;
  double currentBPM;
  List<float> posX = new List<float>();
  List<float> posY = new List<float>();
  List<double> noteCreateTimings = new List<double>();
  List<double> noteTapTimings = new List<double>();
  
  void Start()
  {
    var map = new Map("1", Difficulty.Easy);
    double elapsedTimeUpToThisBar = 0;
    Music music = GameObject.Find("music").AddComponent<Music>();
    music.DebugText = GameObject.Find("status").GetComponent<TextMesh>();
    music.Sections = new List<Music.Section>();

    //Sectionを作成する部分。
    for (int BPMChangeNumber = 0; BPMChangeNumber < map.Command.BPMs.Count; BPMChangeNumber++)
    {
      int startBar = map.Command.BPMs[BPMChangeNumber].Bar;
      double tempo = map.Command.BPMs[BPMChangeNumber].Value;

      music.Sections.Add(new Music.Section(startBar: startBar, mtBeat: UnitPerBeat, mtBar: UnitPerBar, tempo: tempo));
      music.Sections[BPMChangeNumber].Name = "Section" + BPMChangeNumber;
      music.OnVal();
    }

    //ノートを置くタイミングと座標を決める部分。
    for (int aaa = 0; aaa < map.Notes.Count; aaa++)
    {
      double currentBPM = GetCurrentBPM(aaa);
      mtPerSecond = UnitPerBeat * (currentBPM / 60);
      for (int bbb = 0; bbb < map.Notes[aaa].Rhythm.Length; bbb++)
      {
        if (map.Notes[aaa].Rhythm[bbb] == '1')
        {
          bar = (map.Notes[aaa].Bar);
          beat = (bbb / (UnitPerBeat));
          unit = (bbb % (UnitPerBeat));
          noteTapTimings.Add((beat * UnitPerBeat + unit) / mtPerSecond + elapsedTimeUpToThisBar); //単位は秒。
          noteCreateTimings.Add(noteTapTimings[noteTapTimings.Count - 1] - 1); //1は適当に置いてるだけの数字。たたく何秒前にノーツを表示させたいかを入れる。
          posX.Add(map.Notes[aaa].X);
          posY.Add(map.Notes[aaa].Y);
          Debug.Log(bar + " " + beat + " " + unit);
          Debug.Log(noteTapTimings[noteTapTimings.Count - 1] + "秒");
        }
      }
      elapsedTimeUpToThisBar += (map.Notes[aaa].Rhythm.Length / mtPerSecond);
      Debug.Log(elapsedTimeUpToThisBar);
    }
    Music.Play(name, "Section0");
  }

  void Update()
  {
    ElapsedTime += Time.deltaTime;
    if (a < noteCreateTimings.Count)
    {
      if (ElapsedTime >= noteCreateTimings[a])
      {
        NoteManager.CreateNote(posX[a],posY[a], noteTapTimings[a]);
        Debug.Log(ElapsedTime + "秒");
        a++;
      }
    }
  }

  double GetCurrentBPM(int Bar)
  {
    var map = new Map("1", Difficulty.Easy);
    int currentBar = map.Notes[Bar].Bar;
    for (int ccc = 0; ccc < map.Command.BPMs.Count; ccc++)
    {
      if (currentBar < map.Command.BPMs[ccc].Bar)
      {
        currentBPM = map.Command.BPMs[ccc - 1].Value;
        break;
      }else if (currentBar == map.Command.BPMs[ccc].Bar)
        {
          currentBPM = map.Command.BPMs[ccc].Value;
          break;
        }
    }
    return currentBPM;
  }
}
