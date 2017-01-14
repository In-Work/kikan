﻿using UnityEngine;
using System.Collections;

public class LinearMoveSystem : MonoBehaviour {
  void FixedUpdate() {
    if (_actFlag) {
      if (_state.Ground) {
        if (System.Math.Abs(_rigid.velocity.x) <= _limit)
          _rigid.AddForce(_inputVec * _force);
      }

      if (_state.Air) {
        if (System.Math.Abs(_rigid.velocity.x) <= 0.5f)
          _rigid.AddForce(_inputVec * 2.0f);
      }

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

  public bool CanUse {
    get {
      return _state.Ground || _state.Air;
    }
  }

  [SerializeField] private Rigidbody2D _rigid;
  [SerializeField] private RigidState _state;
  private float _force;
  private float _limit;
  private bool _actFlag;
  private Vector2 _inputVec;
}

