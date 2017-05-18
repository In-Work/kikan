﻿using UnityEngine;
using System.Collections;

public class ManjiX : Skill {
  void OnTriggerEnter2D(Collider2D collider) {
    if (PhotonNetwork.isMasterClient) {
      var target = collider.gameObject;

      if (target == _user)
        return;

      if (target.tag == "Player") {
        if (_limiter.Check(target)) {
          var targetHp = target.GetComponent<PlayerHp>();
          targetHp.Minus(10);
          targetHp.Show();

          if (targetHp.Dead) {
            _expGetter.SetExpReceiver(_user, _team);
            _expGetter.GetExpFrom(target);

            _user.GetComponent<PlayerKillDeathRecorder>().RecordKill();
            target.GetComponent<PlayerKillDeathRecorder>().RecordDeath();
          }
        }
      }
    }
  }

  [SerializeField] private BoxCollider2D _collider;
  [SerializeField] private int _power;
  [SerializeField] private TargetLimiter _limiter;
  [SerializeField] private ExpGetter _expGetter;
}

