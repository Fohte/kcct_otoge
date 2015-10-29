using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Otoge.Util;

public class MusicPlayController : MonoBehaviour
{
  public const int UnitPerBar = 16;  //デバッグ用。実際は音符の混合とがあること、拍子の変化に対応できなかったことより、unitPerBarは48、unitPerBeatは12。
  public const int UnitPerBeat = 4;

  public static double ElapsedTime = 0;
  int a = 0;
  double currentBPM;
  float X = 0, Y = 0;  
  List<int> bars = new List<int>();
  List<int> beats = new List<int>();
  List<int> units = new List<int>();
  List<double> mtPerSeconds = new List<double>();
  List<double> noteCreateTimings = new List<double>();
  List<double> noteTapTimings = new List<double>();
  List<string> rhythms = new List<string>()
  {
    "1000100010101111"
    //"000000000000100100100000",// "00 00 11 10"
    //"100100100100100100101010" // "11 11 11 00" + "000 000 000 111"
  };

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

    //ノートを置くタイミングを決める部分。
    for (int aaa = 0; aaa < map.Notes.Count; aaa++)
    {
      for (int bbb = 0; bbb < map.Notes[aaa].Rhythm.Length; bbb++)
      {
        double currentBPM = GetCurrentBPM(aaa);
        mtPerSeconds.Add(UnitPerBeat * currentBPM / 60);

        if (map.Notes[aaa].Rhythm[bbb] == '1')
        {
          bars.Add(map.Notes[aaa].Bar);
          beats.Add(bbb / (UnitPerBeat));
          units.Add(bbb % (UnitPerBeat));
          noteTapTimings.Add((bars[bars.Count - 1] * UnitPerBar + beats[beats.Count - 1] * UnitPerBeat + units[units.Count - 1]) / mtPerSeconds[mtPerSeconds.Count - 1] + elapsedTimeUpToThisBar); //単位は秒。
          noteCreateTimings.Add(noteTapTimings[noteTapTimings.Count - 1] ); //1は適当に置いてるだけの数字。たたく何秒前にノーツを表示させたいかを入れる。 
        }
      }
      elapsedTimeUpToThisBar += (map.Notes[aaa].Rhythm.Length / mtPerSeconds[aaa]);
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
        NoteManager.CreateNote(X, Y, noteTapTimings[a]); //デバッグ用に適当な変数を入れているが、本来は譜面情報に入っている座標を解析して引数に入れる。
        X += 1; //これもデバッグ用。オブジェクトを出す位置をずらして確認しやすくするため。
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
      if (currentBar < map.Command.BPMs[ccc].Bar) currentBPM = map.Command.BPMs[ccc - 1].Value;
      if (currentBar == map.Command.BPMs[ccc].Bar) currentBPM = map.Command.BPMs[ccc].Value;
    }
    return currentBPM;
  }
}
