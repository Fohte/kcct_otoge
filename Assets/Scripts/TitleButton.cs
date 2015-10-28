using UnityEngine;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour
{
  void Start()
  {
    Button button = this.GetComponent<Button>();
    button.onClick.AddListener(() =>
    {
      Debug.Log("Clicked");
    });
  }
}
