﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class LobbyStepDownJumpSMB : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player == null)
        _player = animator.GetComponent<LobbyPlayer>();

      InitFlag();
      _player.FootCollider.isTrigger = true;
      _player.Movement.StepDownJump();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player.PhotonView.isMine) {
        UpdateFlag();
        if ( _player.State.Air && _fallFlag ) { _player.StateTransfer.TransitTo( "Fall" , animator ); return; }
      }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      _player.FootCollider.isTrigger = false;
    }

    private void InitFlag() {
      _isAlreadyJumped = false;
      _fallFlag = false;
    }

    private void UpdateFlag() {
      if (_player.State.Air)
        _isAlreadyJumped = true;

      if (_player.State.Ground && _isAlreadyJumped)
        _fallFlag = true;
    }

    private LobbyPlayer _player;
    private bool _isAlreadyJumped;
    private bool _fallFlag;
  }
}

