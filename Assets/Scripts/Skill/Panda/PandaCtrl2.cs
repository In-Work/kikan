﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(SkillSynchronizer))]
  public class PandaCtrl2 : Skill {
    void Awake() {
      _synchronizer = GetComponent<SkillSynchronizer>();
      _hitRistrictor = new HitRistrictor(_hitInfo);
    }

    void Start() {
      if (photonView.isMine) {
        var direction = new Vector2(-2.5f, 10.0f);

        if (transform.eulerAngles.y == 180)
          direction.x *= -1;

        _synchronizer.SyncForce(_skillUserViewID, _force, direction, true);
      }
    }

    void OnTriggerEnter2D(Collider2D collider) {
      if (PhotonNetwork.isMasterClient) {
        var target = collider.gameObject.GetComponent<IPhoton>();

        if (target == null)
          return;
        if (TeamChecker.IsSameTeam(collider.gameObject, _skillUserObj))
          return;
        if (_hitRistrictor.ShouldRistrict(collider.gameObject))
          return;

        DamageCalculator.Calculate(_skillUserObj, _attackInfo);


        _synchronizer.SyncAttack(_skillUserViewID, target.PhotonView.viewID, DamageCalculator.Damage, DamageCalculator.IsCritical, HitEffectType.Panda);
        _synchronizer.SyncDebuff(target.PhotonView.viewID, DebuffType.Slow, _duration);
      }
    }

    [SerializeField] private AttackInfo _attackInfo;
    [SerializeField] private HitInfo _hitInfo;
    [SerializeField] private float _force;

    private SkillSynchronizer _synchronizer;
    private HitRistrictor _hitRistrictor;
    private float _duration = 2.0f;
  }
}

