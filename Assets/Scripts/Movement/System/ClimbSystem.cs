﻿using UnityEngine;
using System.Collections;

public class ClimbSystem : MonoBehaviour {
  void FixedUpdate() {
    if (_actFlag) {
      _rigid.AddForce(_inputVec * 30.0f);
      _actFlag = false;
    }
  }

  public void MoveUp() {
    _actFlag = true;
    _inputVec.y += 1;

    if (_inputVec.y > 1)
      _inputVec.y = 1;
  }

  public void MoveDown() {
    _actFlag = true;
    _inputVec.y -= 1;

    if (_inputVec.y < -1)
      _inputVec.y = -1;
  }

  public bool CanUse {
    get {
      return _state.Ladder;
    }
  }

  [SerializeField] private Rigidbody2D _rigid;
  [SerializeField] private RigidState _state;
  private bool _actFlag;
  private Vector2 _inputVec;
}

