﻿using UnityEngine;
using System.Collections;

public class AirLinearMoveManipulator : MonoBehaviour {
  void Update() {
    if (_system.CanUse) {
      if (Input.GetKey(KeyCode.LeftArrow))
        _system.MoveLeft();

      if (Input.GetKey(KeyCode.RightArrow))
        _system.MoveRight();
    }
  }

  [SerializeField] private AirLinearMoveSystem _system;
}

