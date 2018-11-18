﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class PlayerAutomaticHealer : Photon.MonoBehaviour {
    void Awake() {
      HealQuantity = _healTable.Data[0];
    }

    void Update() {
      if (photonView.isMine) {
        bool isInjured = (_player.Hp.Cur.Value < _player.Hp.Max.Value);

        if (isInjured)
          AutomaticHeal();
      }
    }

    public void UpdateMaxHealQuantity() {
      Assert.IsTrue(photonView.isMine);

      double ratio = (double)(_player.Core.GetValue(CoreType.Heal) / 100.0);
      HealQuantity = (int)(_healTable.Data[_player.Level.Cur.Value - 1] * ratio);
    }

    private void AutomaticHeal() {
      if (_isActive) return;

      _isActive = true;

      MonoUtility.Instance.DelaySec(HealInterval, () => {
        _isActive = false;

        if (_player.Hp.Cur.Value <= 0 ) return;

        _player.Hp.Add(HealQuantity);
      });
    }

    [SerializeField] private Player _player;
    [SerializeField] private DataTable _healTable;
    public int HealQuantity { get; private set; }

    private bool _isActive = false;
    private static readonly float HealInterval = 1.0f;
  }
}

