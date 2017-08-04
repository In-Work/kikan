﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Math = System.Math;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemySpawner : MonoBehaviour {
  void Awake() {
    float ratioSum = _spawnRatio.Aggregate((x, y) => x + y);
    Assert.AreApproximatelyEqual(ratioSum, 1.0f, 0.001f);
  }

  public void NetworkSpawn(EnemyPopulationObserver populationObserver, float seedX) {
    int index = GetRandomIndex();

    float x = GetRandomPosX(seedX);
    float y = GetRandomPosY();
    var pos = new Vector3(x, y, 0.0f);

    var enemyObj = PhotonNetwork.Instantiate("Prefabs/Enemy/" + _spawnEnemyNames[index], pos, Quaternion.identity, 0);
    var enemy    = enemyObj.GetComponent<Enemy>();
    enemy.AttachPopulationObserver(populationObserver);
  }

  private int GetRandomIndex() {
    int index = 0;
    var threshold = Random.value;

    _spawnRatio.Aggregate((probability, x) => {
      if (probability > threshold) {
        return probability;
      } else {
        index += 1;
        return probability + x;
      }
    });

    return index;
  }

  // Box-Muller Transformation
  private float GetRandomPosX(float seedX) {
    double x1 = Random.value;
    double x2 = Random.value;

    return (float)(_dispersion * Math.Sqrt(-2 * Math.Log(x1)) * Math.Cos(2 * Math.PI * x2));// + seedX);
  }

  private float GetRandomPosY() {
    Debug.Log(gameObject.transform.position);
    return gameObject.transform.position.y;
  }

  [SerializeField] private string[] _spawnEnemyNames;
  [SerializeField] private float[] _spawnRatio; // INFO: e.g. [0.1, 0.9]

  [Space(10)]
  [SerializeField] private float _dispersion;
}

