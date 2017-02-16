﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
  void Start() {
    if (_photonView.isMine)
      gameObject.SetActive(true);
    else
      gameObject.SetActive(false);
  }

  [SerializeField] private PhotonView _photonView;
}

