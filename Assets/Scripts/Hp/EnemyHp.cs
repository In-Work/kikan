﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class EnemyHp : Hp {
    public EnemyHp(Enemy enemy, PlainBar bar, int maxHp) {
      _enemy = enemy;
      _bar = bar;

      Max = maxHp;
      Cur = Max;
    }

    public override void Add(int quantity) {
      base.Add(quantity);
      _enemy.Observer.SyncCurHp();
    }

    public override void Subtract(int quantity) {
      base.Subtract(quantity);
      _enemy.Observer.SyncCurHp();
    }

    public void UpdateView(PhotonPlayer skillOwner) {
      _enemy.Observer.SyncUpdateHpView(skillOwner);
    }

    /*                                                            *
     * INFO: ForceSyncXXX method must be called ONLY by Observer. *
     *       Otherwise it breaks encapsulation.                   *
     *                                                            */
    public void ForceSyncCur(int cur) {
      Assert.IsTrue(_enemy.Observer.ShouldSync("CurHp"));
      Cur = cur;
    }

    public void ForceSyncUpdateView(PhotonPlayer skillOwner) {
      Assert.IsTrue(_enemy.Observer.ShouldSync("UpdateHpView"));

      if (skillOwner == PhotonNetwork.player) {
        _bar.gameObject.SetActive(true);
        _bar.UpdateView(Cur, Max);

        MonoUtility.Instance.OverwritableDelaySec(5.0f, "EnemyHpBarHide" + _enemy.gameObject.GetInstanceID().ToString(), () => {
          _bar.gameObject.SetActive(false);
        });
      }
    }

    private Enemy _enemy;
    private PlainBar _bar;
  }
}

