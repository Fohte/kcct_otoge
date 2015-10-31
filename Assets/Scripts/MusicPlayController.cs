using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Otoge.Util;

//TODO aaa,bbb,ccc,ddd などの適当な変数名を直す

public class MusicPlayController : MonoBehaviour
{
  public const int UnitPerBar = 16;  //デバッグ用。実際は音符の混合と拍子の変化がないことから、unitPerBarは48、unitPerBeatは12。
  public const int UnitPerBeat = 4;

  const double MaxScore = 1000000;
  const double Perfect = 1.0;
  const double Great = 0.7;
  const double Good = 0.5;
  const double Bad = 0.1;
  const double Miss = 0.0;

  public static double ElapsedTime = 0;
  public static int perfect = 0;
  public static int great = 0;
  public static int good = 0;
  public static int bad = 0;
  public static int miss = 0;

  int ddd = 0;
  int bar = 0;
  int beat = 0;
  int unit = 0;
  int lastBar = 1000;
  static int combo = 0;
  static double score = 0;
  static double scorePerNote = 0;
  static double currentScore = 0;
  double lastElapsedTimeUpToThisBar = 0;
  double mtPerSecond = 0;
  double currentBPM = 0;
  List<float> posX = new List<float>();
  List<float> posY = new List<float>();
  List<double> noteCreateTimings = new List<double>();
  List<double> noteTapTimings = new List<double>();

  public TextMesh comboAndScoreText;

  void Start()
  {
    var map = new Map("1", Difficulty.Easy);
    double elapsedTimeUpToThisBar = 0;
    Music music = GameObject.Find("Music").AddComponent<Music>();
    music.DebugText = GameObject.Find("Status").GetComponent<TextMesh>();
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
          noteCreateTimings.Add(noteTapTimings[noteTapTimings.Count - 1] - 0.5 ); //0.5は適当に置いてるだけの数字。たたく何秒前にノーツを表示させたいかを入れる。
          posX.Add(map.Notes[aaa].X);
          posY.Add(map.Notes[aaa].Y);
        }
      }
      lastElapsedTimeUpToThisBar = elapsedTimeUpToThisBar;
      elapsedTimeUpToThisBar += (map.Notes[aaa].Rhythm.Length / mtPerSecond);
      lastBar = bar;
    }
    scorePerNote = MaxScore / noteCreateTimings.Count;
    Debug.Log(scorePerNote);
    Music.Play(name, "Section0");
  }

  void Update()
  {
    comboAndScoreText.text = "コンボ数:" + combo + "スコア:" + currentScore;
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

  public static void comboAndScoreUpdate()
  {
    switch (JudgeManager.judge.ToString())
    {
      case "Perfect":
        combo++;
        perfect++;
        score += Perfect * scorePerNote;
        break;
      case "Great":
        combo++;
        great++;
        score += Great * scorePerNote;
        break;
      case "Good":
        combo++;
        good++;
        score += Good * scorePerNote;
        break;
      case "Bad":
        combo = 0;
        bad++;
        score += Bad * scorePerNote;
        break;
      case "Miss":
        combo = 0;
        miss++;
        break;
    }
    currentScore = Math.Ceiling(score);
  }
}
