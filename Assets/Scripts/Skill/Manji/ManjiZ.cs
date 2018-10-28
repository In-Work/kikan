﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class ManjiZ : Skill {
    void Awake() {
      _targetRistrictor  = new TargetRistrictor(_targetNum, _dupHitNum);
      _killDeathRecorder = new KillDeathRecorder();
    }

    void OnTriggerEnter2D(Collider2D collider) {
      if (!PhotonNetwork.isMasterClient)
        return;

      var targetObj = collider.gameObject;
      if (targetObj == _skillUserObj)
        return;

      if (targetObj.tag == "Player")
        ProceedAttackToPlayer(targetObj);

      if (targetObj.tag == "Enemy")
        ProceedAttackToEnemy(targetObj);
    }

    private void ProceedAttackToPlayer(GameObject targetObj) {
      var target = targetObj.GetComponent<BattlePlayer>();
      var skillUser = _skillUserObj.GetComponent<BattlePlayer>();

      if (IsCorrectAttackPlayer(target, skillUser)) {
        DamageToPlayer(target, skillUser);
        target.BuffState.ToBeStun(_stunSec);
        //target.NumberPopupEnvironment.Popup(DamageCalculator.Damage, DamageCalculator.IsCritical, skillUser.DamageSkin.Id, PopupType.Player);

        if (target.Hp.Cur <= 0)
          ProceedPlayerDeath(target, skillUser);
      }
    }

    private void ProceedAttackToEnemy(GameObject targetObj) {
      var target = targetObj.GetComponent<Enemy>();
      var skillUser = _skillUserObj.GetComponent<BattlePlayer>();

      if (IsCorrectAttackEnemy(target)) {
        DamageToEnemy(target, skillUser);
        target.BuffState.ToBeStun(_stunSec);
        //target.NumberPopupEnvironment.Popup(DamageCalculator.Damage, DamageCalculator.IsCritical, skillUser.DamageSkin.Id, PopupType.Enemy);

        if (target.Hp.Cur <= 0)
          ProceedEnemyDeath(target, skillUser);
      }
    }

    private bool IsCorrectAttackPlayer(BattlePlayer target, BattlePlayer skillUser) {
      if (target.PlayerInfo.Team == skillUser.PlayerInfo.Team)
        return false;
      if (_targetRistrictor.ShouldRistrict(target))
        return false;

      return true;
    }

    private bool IsCorrectAttackEnemy(Enemy target) {
      if (_targetRistrictor.ShouldRistrict(target))
        return false;

      return true;
    }

    private void DamageToPlayer(BattlePlayer target, BattlePlayer skillUser) {
      DamageCalculator.Calculate(_skillUserObj, _attackInfo);

      //target.Hp.Subtract(DamageCalculator.Damage);
      target.Hp.UpdateView();
    }

    private void DamageToEnemy(Enemy target, BattlePlayer skillUser) {
      DamageCalculator.Calculate(_skillUserObj, _attackInfo);

      //target.Hp.Subtract(DamageCalculator.Damage);
      target.Hp.UpdateView(skillUser.PhotonView.owner);
    }

    private void ProceedPlayerDeath(BattlePlayer target, BattlePlayer skillUser) {
      _killDeathRecorder.RecordKillDeath(target, skillUser);
    }

    private void ProceedEnemyDeath(Enemy target, BattlePlayer skillUser) {
    }

    [SerializeField] private AttackInfo _attackInfo;

    [Header("TargetSettings")]
    [SerializeField] private int _targetNum;
    [SerializeField] private int _dupHitNum;

    [Space(10)]
    [SerializeField] private float _stunSec;

    private TargetRistrictor  _targetRistrictor;
    private KillDeathRecorder _killDeathRecorder;
  }
}

