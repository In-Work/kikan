﻿using UnityEngine;
using System.Collections;

public class ManjiX : DamageSkill {
  protected override void Awake() {
    _damageBehaviour = new DamageBehaviour();
    _rewardGetter = new RewardGetter();
  }

  void OnTriggerEnter2D(Collider2D collider) {
    if (PhotonNetwork.isMasterClient) {
      var target = collider.gameObject;

      if (target == _skillUser)
        return;

      if (target.tag == "Player" && _limiter.Check(target, _team))
        DamageToPlayer(_power, _maxDeviation, target);
    }
  }

  [SerializeField] private TargetLimiter _limiter;
  [SerializeField] private int _power;
  [SerializeField] private int _maxDeviation;
}

