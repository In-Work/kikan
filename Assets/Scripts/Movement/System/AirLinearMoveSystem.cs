﻿using UnityEngine;
using System.Collections;

public class AirLinearMoveSystem : ScriptableObject {
  public void CallFixedUpdate(Rigidbody2D rigid) {
    if (_actFlag) {
      if (System.Math.Abs(rigid.velocity.x) <= _limit)
        rigid.AddForce(_inputVec * _force);

      _actFlag = false;
    }
  }

  public void MoveLeft() {
    _actFlag = true;
    _inputVec.x -= 1;

    if (_inputVec.x < -1)
      _inputVec.x = -1;
  }

  public void MoveRight() {
    _actFlag = true;
    _inputVec.x += 1;

    if (_inputVec.x > 1)
      _inputVec.x = 1;
  }

  public void SetForce(float force) {
    _force = force;
  }

  public void SetLimit(float limit) {
    _limit = limit;
  }

  private float _force;
  private float _limit;
  private bool _actFlag;
  private Vector2 _inputVec;
}

