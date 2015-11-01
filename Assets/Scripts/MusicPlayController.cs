using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Otoge.Util;

//TODO aaa,bbb,ccc,ddd などの適当な変数名を直す

public class MusicPlayController : MonoBehaviour
{
  public const int UnitPerBar = 48;  //デバッグ用。実際は音符の混合と拍子の変化がないことから、unitPerBarは48、unitPerBeatは12。
  public const int UnitPerBeat = 12;

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
  public static double currentScore = 0;
  public static int combo = 0;
  public static string grade;

  int ddd = 0;
  int bar = 0;
  int beat = 0;
  int unit = 0;
  int lastBar = 1000;
  static double score = 0;
  static double scorePerNote = 0;
  double lastElapsedTimeUpToThisBar = 0;
  double mtPerSecond = 0;
  double currentBPM = 0;
  double finalNoteCreateTiming;
  List<float> posX = new List<float>();
  List<float> posY = new List<float>();
  List<double> noteCreateTimings = new List<double>();
  List<double> noteTapTimings = new List<double>();
  List<string> kindOfNote = new List<string>();

  void Start()
  {
    ElapsedTime = 0;
    var map = new Map("1", Difficulty.Hard);
    double elapsedTimeUpToThisBar = 0;
    Music music = GameObject.Find("Music").AddComponent<Music>();
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
          noteTapTimings.Add((beat * UnitPerBeat + unit) / mtPerSecond + elapsedTimeUpToThisBar + (map.Header.Offset / 1000)); //単位は秒。
          noteCreateTimings.Add(noteTapTimings[noteTapTimings.Count - 1] - 0.5 ); //0.5は適当に置いてるだけの数字。たたく何秒前にノーツを表示させたいかを入れる。
          posX.Add(map.Notes[aaa].X);
          posY.Add(map.Notes[aaa].Y);
          kindOfNote.Add(map.Notes[aaa].Type);
          Debug.Log(map.Header.Offset / 1000);
          Debug.Log(noteCreateTimings[noteCreateTimings.Count - 1]);
          Debug.Log(posX[posX.Count - 1]);
          Debug.Log(posY[posY.Count - 1]);
          Debug.Log(kindOfNote[kindOfNote.Count - 1]);
          finalNoteCreateTiming = noteCreateTimings[noteCreateTimings.Count - 1];
        }
      }
      lastElapsedTimeUpToThisBar = elapsedTimeUpToThisBar;
      elapsedTimeUpToThisBar += (map.Notes[aaa].Rhythm.Length / mtPerSecond);
      lastBar = bar;
    }
    scorePerNote = MaxScore / noteCreateTimings.Count;
    Debug.Log(finalNoteCreateTiming);
    Music.Play(name, "Section0");
  }

  void Update()
  {
    ElapsedTime += Time.deltaTime;
    if (ElapsedTime >= finalNoteCreateTiming + 3)
    {
        if (currentScore >= 990000)//AAA+
        {
          grade = "AAA+";
        }
        else if (currentScore >= 980000)
        {
          grade = "AAA";
        }
        else if (currentScore >= 950000)
        {
          grade = "AA";
        }
        else if (currentScore >= 900000)
        {
          grade = "A";
        }
        else if (currentScore >= 800000)
        {
          grade = "B";
        }
        else
        {
          grade = "C";
        }
      Debug.Log(grade);
      Application.LoadLevel("Result");
    }
 
    if (ddd < noteCreateTimings.Count)
    {
      if (ElapsedTime >= noteCreateTimings[ddd])
      {
        for (int eee = ddd + 1; eee < ddd + (UnitPerBar * 4); eee++) //いくつまでノートを同時に押したいかによってここの1という数字は変わる
        {
          if (eee < noteCreateTimings.Count)
          {
            if (noteCreateTimings[eee] == noteCreateTimings[ddd])
            {   
              NoteManager.CreateNote(posX[eee], posY[eee], noteTapTimings[eee],kindOfNote[eee]);
              noteCreateTimings.RemoveAt(eee);
              noteTapTimings.RemoveAt(eee);
              posX.RemoveAt(eee);
              posY.RemoveAt(eee);
            }
          }
        }
        NoteManager.CreateNote(posX[ddd],posY[ddd], noteTapTimings[ddd],kindOfNote[ddd]);
        ddd++;
      }
    }
  }

  double GetCurrentBPM(int Bar)
  {
    var map = new Map("1", Difficulty.Hard);
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
