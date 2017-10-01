﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class PlayerLevel : Level {
    public void Init(LevelPanel lvPanel, KillDeathPanel kdPanel) {
      Assert.IsTrue(photonView.isMine);

      int initialLv = 1;
      Init(initialLv);

      _lvPanel = lvPanel;
      _kdPanel = kdPanel;
    }

    public void UpdateView() {
      Assert.IsTrue(photonView.isMine);

      _lvPanel.UpdateView(Lv);
      _kdPanel.UpdateLvView(Lv);
    }

    public override void LvUp() {
      base.LvUp();

      if (photonView.isMine) {
        UpdateView();

        _player.Hp.UpdateMaxHp();
        _player.Observer.SyncMaxHp();

        _player.Hp.UpdateView();
        _player.Observer.SyncUpdateHpView();

        _playerHealer.UpdateMaxHealQuantity();
      }
    }

    [SerializeField] private BattlePlayer _player;
    [SerializeField] private PlayerAutomaticHealer _playerHealer;
    private LevelPanel _lvPanel;
    private KillDeathPanel _kdPanel;
  }
}

