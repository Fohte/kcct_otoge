using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Otoge.Util;

//TODO aaa,bbb,ccc,ddd などの適当な変数名を直す

public class MusicPlayController : MonoBehaviour
{
  public const int UnitPerBar = 16;  //デバッグ用。実際は音符の混合と拍子の変化がないことから、unitPerBarは48、unitPerBeatは12。
  public const int UnitPerBeat = 4;
  public static double ElapsedTime = 0;
  int ddd = 0;
  int bar = 0;
  int beat = 0;
  int unit = 0;
  int lastBar = 1000;
  double lastElapsedTimeUpToThisBar = 0;
  double mtPerSecond = 0;
  double currentBPM = 0;
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
      currentBPM = GetCurrentBPM(aaa);
      mtPerSecond = UnitPerBeat * (currentBPM / 60);
      bar = (map.Notes[aaa].Bar);
      if (bar == lastBar)
      {
        elapsedTimeUpToThisBar = lastElapsedTimeUpToThisBar;
      }
      for (int bbb = 0; bbb < map.Notes[aaa].Rhythm.Length; bbb++)
      {
        if (map.Notes[aaa].Rhythm[bbb] == '1')
        {
          beat = (bbb / (UnitPerBeat));
          unit = (bbb % (UnitPerBeat));
          noteTapTimings.Add((beat * UnitPerBeat + unit) / mtPerSecond + elapsedTimeUpToThisBar); //単位は秒。
          noteCreateTimings.Add(noteTapTimings[noteTapTimings.Count - 1] ); //1は適当に置いてるだけの数字。たたく何秒前にノーツを表示させたいかを入れる。
          posX.Add(map.Notes[aaa].X);
          posY.Add(map.Notes[aaa].Y);
        }
      }
      lastElapsedTimeUpToThisBar = elapsedTimeUpToThisBar;
      elapsedTimeUpToThisBar += (map.Notes[aaa].Rhythm.Length / mtPerSecond);
      lastBar = bar;
    }
    Music.Play(name, "Section0");
  }

  void Update()
  {
    var map = new Map("1", Difficulty.Easy);
    ElapsedTime += Time.deltaTime;
    if (ddd < noteCreateTimings.Count)
    {
      if (ElapsedTime >= noteCreateTimings[ddd])
      {
        for (int eee = ddd + 1; eee < ddd + (UnitPerBar * 1); eee++) //いくつまでノートを同時に押したいかによってここの1という数字は変わる
        {
          if (eee < noteCreateTimings.Count)
          {
            if (noteCreateTimings[eee] == noteCreateTimings[ddd])
            {   
              NoteManager.CreateNote(posX[eee], posY[eee], noteTapTimings[eee]);
              noteCreateTimings.RemoveAt(eee);
              noteTapTimings.RemoveAt(eee);
              posX.RemoveAt(eee);
              posY.RemoveAt(eee);
            }
          }
        }
        NoteManager.CreateNote(posX[ddd],posY[ddd], noteTapTimings[ddd]);
        ddd++;
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
