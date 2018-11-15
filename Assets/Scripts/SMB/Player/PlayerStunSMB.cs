﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class PlayerStunSMB : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player == null)
        _player = animator.GetComponent<Player>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player.PhotonView.isMine) {
        if (!_player.BuffState.Stun) {
          if ( _player.Location.IsGround ) { _player.StateTransfer.TransitTo( "Idle" , animator ); return; }
          if ( _player.Location.IsAir    ) { _player.StateTransfer.TransitTo( "Fall" , animator ); return; }
        }
      }
    }

    private Player _player;
  }
}

