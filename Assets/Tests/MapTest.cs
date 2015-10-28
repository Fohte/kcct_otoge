using UnityEngine;
using System.Collections;
using Otoge.Util;

public class MapTest : MonoBehaviour
{
  void Start()
  {
    var map = new Map();
    map.Load("1", Difficulty.Easy);
    Debug.Log(map.MapFilePath);

    map.Save(null, null, null, "2", Difficulty.Hard);
    Debug.Log(map.MapFilePath);
  }
}
