﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : Photon.MonoBehaviour {
  void Awake() {
    _reconfirmList = new List<bool>() { false, false, false, false, false };
  }

  void Update() {
    if (photonView.isMine == false)
      return;

    for (int i=0; i<_coreList.Count; ++i) {
      bool lvUpRequest = Input.GetKeyDown(_coreList[i].Key);
      if (lvUpRequest == false)
        continue;

      if (_reconfirmList[i] == false) {
        _reconfirmList[i] = true;
        return;
      }

      bool notEnoughGold = (_playerGold.Cur < _coreList[i].Gold);
      if (notEnoughGold) {
        Debug.Log("You don't have enough gold");
        return;
      }

      bool isMaxLevel = (_coreList[i].Level == MaxCoreLevel);
      if (isMaxLevel) {
        Debug.Log("Your core level is already max");
        return;
      }

      _playerGold.Minus(_coreList[i].Gold);
      _playerGold.UpdateView();

      _coreList[i].LvUp();
      UpdateView(i, _coreList[i].Level);
      _reconfirmList[i] = false;
    }
  }

  public void Init(CorePanel corePanel) {
    _corePanel = corePanel;
  }

  private void UpdateView(int index, int level) {
    _corePanel.ChartList[index].UpdateView(level);
  }

  public int Speed {
    get {
      return _coreList[0].Value;
    }
  }

  public int Hp {
    get {
      return _coreList[1].Value;
    }
  }

  public int Attack {
    get {
      return _coreList[2].Value;
    }
  }

  public int Critical {
    get {
      return _coreList[3].Value;
    }
  }

  public int Heal {
    get {
      return _coreList[4].Value;
    }
  }

  [SerializeField] private PlayerGold _playerGold;
  [SerializeField] private List<Core> _coreList;
  private CorePanel _corePanel;
  private List<bool> _reconfirmList;
  private static readonly int MaxCoreLevel = 5;
}

