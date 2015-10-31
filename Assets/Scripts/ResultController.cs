using UnityEngine;
using Otoge.Util;
using UnityEngine.UI;

public class ResultController : MonoBehaviour
{
  void Start()
  {
    //var map = new Map(MusicSelectController.MusicId, Difficulty.Hard);
    var map = new Map("1", Difficulty.Hard);
    var title = GameObject.Find("Title").GetComponent<Text>();
    var creator = GameObject.Find("MusicCreator").GetComponent<Text>();

    GameObject.Find("Grade").GetComponent<Text>().text = "0";
    GameObject.Find("Score_value").GetComponent<Text>().text = "0";
    GameObject.Find("Combo_value").GetComponent<Text>().text = "0";
    GameObject.Find("Perfect_value").GetComponent<Text>().text = "0";
    GameObject.Find("Great_value").GetComponent<Text>().text = "0";
    GameObject.Find("Good_value").GetComponent<Text>().text = "0";
    GameObject.Find("Bad_value").GetComponent<Text>().text = "0";
    GameObject.Find("Miss_value").GetComponent<Text>().text = "0";

    title.text = map.Header.Title;
    creator.text = map.Header.MusicArtist;
    Image image = GameObject.Find("Jacket").gameObject.GetComponent<Image>();
    image.sprite = Resources.Load<Sprite>("Jackets/" + map.Header.JacketFile);

    GameObject.Find("NextButton").GetComponent<Button>().onClick.AddListener(() =>
    {
      Application.LoadLevel("MusicSelect");
    });
  }
}
