﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class ManjiSpace : Skill {
    void Start() {
      transform.parent = _skillUserObj.transform;

      if (photonView.isMine) {
        EnhanceStatus();
        InstantiateBuff();
      }
    }

    void OnDestroy() {
      if (photonView.isMine) {
        ResetStatus();
        SkillReference.Instance.Remove(this);
      }
    }

    private void EnhanceStatus() {
      var skillUser = _skillUserObj.GetComponent<BattlePlayer>();

      float statusRatio = 1.3f;
      float powerRatio = 1.5f;
      if (skillUser.Level.Cur >= 11)
        statusRatio = 1.6f;
        powerRatio = 2.0f;

      skillUser.Movement.SetMoveForce(skillUser.Status.Spd * statusRatio);
      skillUser.Movement.SetJumpForce(skillUser.Status.Jmp * statusRatio);

      skillUser.Status.MultipleMulCorrectionAtk(powerRatio);

      ResetStatus = () => {
        skillUser.Movement.SetMoveForce(skillUser.Status.Spd);
        skillUser.Movement.SetJumpForce(skillUser.Status.Jmp);

        skillUser.Status.ResetMulCorrectionAtk();
      };

      SkillReference.Instance.Register(this, 20.0f, () => { PhotonNetwork.Destroy(gameObject); });
    }

    private void InstantiateBuff() {
      var skillUser = _skillUserObj.GetComponent<BattlePlayer>();

      var buff = PhotonNetwork.Instantiate("Prefabs/Skill/Manji/SpaceBuff", Vector3.zero, Quaternion.identity, 0).GetComponent<ManjiSpaceBuff>() as ManjiSpaceBuff;
      buff.ParentSetter.SetParent(skillUser.PhotonView.viewID);

      SkillReference.Instance.Register(buff, 20.0f, () => { PhotonNetwork.Destroy(buff.gameObject); });
    }

    private Action ResetStatus;
  }
}

