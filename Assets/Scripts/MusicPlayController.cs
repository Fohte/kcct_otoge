using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicPlayController : MonoBehaviour
{
  public static double elapsedTime = 0;
  int startBar;
  int barNumber = 0, musicalNoteNumber = 0, a = 0;
  float X = 0, Y = 0;  
  List<int> bars = new List<int>();
  List<int> beats = new List<int>();
  List<int> units = new List<int>();
  List<int> tempos = new List<int>();
  List<double> mtPerSeconds = new List<double>();
  List<double> noteCreateTimings = new List<double>();
  List<double> noteTapTimings = new List<double>();
  List<string> rhythms = new List<string>()
  {
    "000000000000100100100000",// "00001110"
    "100100100100100100101010" //"11111100" + "000000000111"                     
  };

  void Start()
  {
    double elapsedTimeUpToThisBar = 0;
    Music music = GameObject.Find("music").AddComponent<Music>();
    music.DebugText = GameObject.Find("status").GetComponent<TextMesh>();
    music.Sections = new List<Music.Section>();

    for (barNumber = 0; barNumber < rhythms.Count; barNumber++)
    {
      int unitPerBar = rhythms[barNumber].Length;
      int unitPerBeat = rhythms[barNumber].Length / 4;

      //Sectionを作る部分。実際はBPMとかが変化したときにSectionを追加作成するようにする。BPMも実際は譜面情報からBPMを読み取って入れる。
      tempos.Add(60 * (barNumber + 1));
      music.Sections.Add(new Music.Section(startBar: barNumber, mtBeat: unitPerBeat, mtBar: unitPerBar, tempo: tempos[tempos.Count - 1]));
      music.Sections[barNumber].Name = "Section" + barNumber;
      music.OnVal();
      mtPerSeconds.Add(unitPerBeat * tempos[tempos.Count - 1] / 60); //1分は60秒。unitPerBeatにBPMを掛け算することで1分当たりのmtが出るのでそれを1秒あたりに変えるために60で割る。 

      //Temporary Noteを作るタイミングを決める部分。
      for (musicalNoteNumber = 0; musicalNoteNumber < rhythms[barNumber].Length; musicalNoteNumber++)
      {
        if (rhythms[barNumber][musicalNoteNumber] == '1')
        {
          bars.Add(barNumber);
          beats.Add(musicalNoteNumber / (unitPerBeat));
          units.Add(musicalNoteNumber % (unitPerBeat));
          noteTapTimings.Add((beats[beats.Count - 1] * unitPerBeat + units[units.Count - 1]) / mtPerSeconds[barNumber] + elapsedTimeUpToThisBar);
          noteCreateTimings.Add(noteTapTimings[noteTapTimings.Count - 1]); //1は適当に置いてるだけの数字。たたく何秒前にノーツを表示させたいかを入れる。
        }
      }
      elapsedTimeUpToThisBar = elapsedTimeUpToThisBar + (rhythms[barNumber].Length / mtPerSeconds[barNumber]);
    }
    barNumber = 0;
    Music.Play(name, "Section0");
  }

  void Update()
  {
    elapsedTime = elapsedTime + Time.deltaTime;
    if (a < noteCreateTimings.Count)
    {
      if (elapsedTime >= noteCreateTimings[a])
      {
        NoteManager.CreateNote(X, Y, noteTapTimings[a]); //デバッグ用に適当な変数を入れているが、本来は譜面情報に入っている座標を解析して引数に入れる。
        X += 1; //これもデバッグ用。オブジェクトを出す位置をずらして確認しやすくするため。
        a++;
      }
    }
  }
}
