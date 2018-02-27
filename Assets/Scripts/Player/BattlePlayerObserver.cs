﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class BattlePlayerObserver : MonoBehaviour {
    void Awake() {
      _shouldSync = new Dictionary<string, bool>();

      _shouldSync.Add("Hp"           , false);
      _shouldSync.Add("CurHp"        , false);
      _shouldSync.Add("MaxHp"        , false);
      _shouldSync.Add("UpdateHpView" , false);
      _shouldSync.Add("Team"         , false);
    }

    public bool ShouldSync(string key) {
      Assert.IsTrue(_shouldSync.ContainsKey(key));
      return _shouldSync[key];
    }

    /* Hp */
    public void SyncHp() {
      _player.PhotonView.RPC("SyncHpRPC", PhotonTargets.Others, _player.Hp.Cur, _player.Hp.Max);
    }

    public void SyncCurHp() {
      _player.PhotonView.RPC("SyncCurHpRPC", PhotonTargets.Others, _player.Hp.Cur);
    }

    public void SyncMaxHp() {
      _player.PhotonView.RPC("SyncMaxHpRPC", PhotonTargets.Others, _player.Hp.Max);
    }

    public void SyncUpdateHpView() {
      _player.PhotonView.RPC("SyncUpdateHpViewRPC", PhotonTargets.All);
    }

    /* Team */
    public void SyncTeam() {
      _player.PhotonView.RPC("SyncTeamRPC", PhotonTargets.Others, _player.PlayerInfo.Team);
    }

    /* Buff */
    public void SyncStun() {
      _player.PhotonView.RPC("SyncStunRPC", PhotonTargets.Others, _player.BuffState.Stun);
    }

    /* Hp RPC */
    [PunRPC]
    private void SyncHpRPC(int cur, int max) {
      ForceSync("Hp", () => _player.Hp.ForceSync(cur, max));
    }

    [PunRPC]
    private void SyncCurHpRPC(int cur) {
      ForceSync("CurHp", () => _player.Hp.ForceSyncCur(cur));
    }

    [PunRPC]
    private void SyncMaxHpRPC(int max) {
      ForceSync("MaxHp", () => _player.Hp.ForceSyncMax(max));
    }

    [PunRPC]
    private void SyncUpdateHpViewRPC() {
      ForceSync("UpdateHpView", () => _player.Hp.ForceSyncUpdateView());
    }

    /* Team RPC */
    [PunRPC]
    private void SyncTeamRPC(int team) {
      ForceSync("Team", () => _player.PlayerInfo.ForceSync(team));
    }

    /* Buff RPC */
    [PunRPC]
    private void SyncStunRPC(bool state) {
      _player.BuffState.Stun = state;
    }

    private void ForceSync(string key, Action action) {
      _shouldSync[key] = true;
      action();
      _shouldSync[key] = false;
    }

    [SerializeField] private BattlePlayer _player;
    private Dictionary<string, bool> _shouldSync;
  }
}

