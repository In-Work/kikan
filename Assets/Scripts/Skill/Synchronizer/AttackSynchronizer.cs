﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class AttackSynchronizer : Photon.MonoBehaviour {
    [PunRPC]
    private void SyncAttackRPC(int attackerViewID, int targetViewID, int damage, bool isCritical) {
      var attacker = PhotonView.Find(attackerViewID).gameObject.GetComponent<IBattle>();
      var target = PhotonView.Find(targetViewID).gameObject.GetComponent<IBattle>();

      Assert.IsNotNull(attacker);
      Assert.IsNotNull(target);

      target.OnAttacked(attacker, damage, isCritical);
    }

    public void SyncAttack(int attackerViewID, int targetViewID, int damage, bool isCritical) {
      Assert.IsTrue(PhotonNetwork.isMasterClient);

      photonView.RPC("SyncAttackRPC", PhotonTargets.All, attackerViewID, targetViewID, damage, isCritical);
    }
  }
}

