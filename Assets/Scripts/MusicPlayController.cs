using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicPlayController : MonoBehaviour
{
  int startBar;
  public static List<bool> createNoteThisTiming = new List<bool>();
  List<string> rhythms = new List<string>()
  {
    "100100100100100100101010" //"11111100" + "000000000111"                     
  };

  int barNumber = 0, musicalNoteNumber = 0;
  float X = 0,Y = 0;
  bool timing;
  List<int> bar = new List<int>();
  List<int> beat = new List<int>();
  List<int> unit = new List<int>();

  void Start()
  {
    Music music = GameObject.Find("music").AddComponent<Music>();
    music.DebugText = GameObject.Find("status").GetComponent<TextMesh>();
    music.Sections = new List<Music.Section>();

    for (barNumber = 0; barNumber < rhythms.Count; barNumber++)
    {
      int UnitPerBeat = rhythms[barNumber].Length / 4;
      int UnitPerBar = rhythms[barNumber].Length;

      music.Sections.Add(new Music.Section(startBar: barNumber, mtBeat: UnitPerBeat, mtBar: UnitPerBar, tempo: 120));
      music.Sections[barNumber].Name = "Section" + barNumber;
      music.OnVal();
      for (musicalNoteNumber = 0; musicalNoteNumber < rhythms[barNumber].Length; musicalNoteNumber++)
      {
        if (rhythms[barNumber][musicalNoteNumber] == '1')
        {
          bar.Add(barNumber);
          beat.Add(musicalNoteNumber / (UnitPerBeat));
          unit.Add(musicalNoteNumber % (UnitPerBeat));
        }
      }
    }
    Music.Play(name, "Section0");
  }
  void Update()
  {
    for (barNumber = 0; barNumber < bar.Count; barNumber++)
    {
      timing = Music.IsJustChangedAt(bar: bar[barNumber], beat: beat[barNumber], unit: unit[barNumber]);
      if (timing)
      {
        Note.SetNoteParBeat(X,Y); //デバッグ用に適当な変数を入れているが、本来は譜面フォーマットに入っている座標情報を解析して引数に入れる。
        X += 0.5f; //これもデバッグ用。オブジェクトを出す位置をずらして確認しやすくするため。
      }
    }
  }
}
