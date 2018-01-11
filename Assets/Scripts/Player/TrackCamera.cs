﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class TrackCamera : MonoBehaviour {
    void Start() {
      transform.position = transform.position + _positionOffset;
    }

    void Update() {
      Vector3 trackPosition = _trackObj.transform.position + _positionOffset;

      if (transform.position != trackPosition) {
        InterpolateTo(trackPosition);
        RestrictEdgeBehaviour();
      }
    }

    public void Init(GameObject trackObj) {
      _trackObj = trackObj;
    }

    private void InterpolateTo(Vector3 destination) {
      Vector3 interpolatedPosition = transform.position * _interpolateRatio + destination * (1.0f - _interpolateRatio);
      transform.position = interpolatedPosition;
    }

    private void RestrictEdgeBehaviour() {
      float distance = Mathf.Abs(transform.position.z);
      Vector3 cameraViewBottomLeft  = _camera.ViewportToWorldPoint(new Vector3(0, 0, distance));
      Vector3 cameraViewTopRight    = _camera.ViewportToWorldPoint(new Vector3(1, 1, distance));
      Vector3 cameraViewTopLeft     = new Vector3(cameraViewBottomLeft.x, cameraViewTopRight.y, cameraViewTopRight.z);
      Vector3 cameraViewBottomRight = new Vector3(cameraViewTopRight.x, cameraViewBottomLeft.y, cameraViewTopRight.z);
      float cameraViewWidth  = cameraViewTopRight.x - cameraViewTopLeft.x;
      float cameraViewHeight = cameraViewTopLeft.y - cameraViewBottomLeft.y;

      float nextX = Mathf.Clamp(transform.position.x, _gameData.StageRect.xMin + cameraViewWidth/2, _gameData.StageRect.xMax - cameraViewWidth/2);
      float nextY = Mathf.Clamp(transform.position.y, _gameData.StageRect.yMin + cameraViewHeight/2 - 2, _gameData.StageRect.yMax - cameraViewHeight/2); // -2 is bottom base sprite height.

      transform.position = new Vector3(nextX, nextY, transform.position.z);
    }

    [SerializeField] private Camera _camera;
    [SerializeField] private GameData _gameData;
    [SerializeField] private Vector3 _positionOffset;
    [SerializeField] private float _interpolateRatio;
    private GameObject _trackObj;
  }
}

