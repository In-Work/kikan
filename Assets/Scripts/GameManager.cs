﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
  void Awake() {
    if (instance == null)
      instance = this;
    else if (instance != this)
      Destroy(gameObject);

    DontDestroyOnLoad(gameObject);
  }

  public static GameManager instance = null;
}

