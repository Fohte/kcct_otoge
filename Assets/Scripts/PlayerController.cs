using UnityEngine;
using Otoge.Util;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
  void Start()
  {
    var map = new Map("1", Difficulty.Easy);
    var title = GameObject.Find("Title").GetComponent<Text>();
    var difficulty = GameObject.Find("Difficulty").GetComponent<Text>();

    title.text = map.Header.Title;
    difficulty.text = map.Difficulty.ToString().ToUpper();
    
    Image image = GameObject.Find("Image").gameObject.GetComponent<Image>();
    image.sprite = Resources.Load<Sprite>("Jackets/" + map.Header.JacketFile);
  }
}
