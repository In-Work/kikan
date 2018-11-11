﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class BattleLieDownSMB : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player == null)
        _player = animator.GetComponent<BattlePlayer>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player.PhotonView.isMine) {
        if ( _player.Hp.Cur.Value <= 0           ) { _player.StateTransfer.TransitTo( "Die"          , animator ); return; }
        if ( _player.BuffState.Stun        ) { _player.StateTransfer.TransitTo( "Stun"         , animator ); return; }
        if ( ShouldTransitToSkill()        ) { _player.StateTransfer.TransitTo( "Skill"        , animator ); return; }
        if ( ShouldTransitToWalk()         ) { _player.StateTransfer.TransitTo( "Walk"         , animator ); return; }
        if ( ShouldTransitToStepDownJump() ) { _player.StateTransfer.TransitTo( "StepDownJump" , animator ); return; }
        if ( ShouldTransitToIdle()         ) { _player.StateTransfer.TransitTo( "Idle"         , animator ); return; }
        if ( _player.State.Air             ) { _player.StateTransfer.TransitTo( "Fall"         , animator ); return; }
      }
    }

    private bool ShouldTransitToSkill() {
      bool SkillFlag = ( _player.SkillInfo.GetState ( SkillName.X     ) == SkillState.Using ) ||
                       ( _player.SkillInfo.GetState ( SkillName.Shift ) == SkillState.Using ) ||
                       ( _player.SkillInfo.GetState ( SkillName.Z     ) == SkillState.Using ) ||
                       ( _player.SkillInfo.GetState ( SkillName.Ctrl  ) == SkillState.Using ) ||
                       ( _player.SkillInfo.GetState ( SkillName.Space ) == SkillState.Using ) ||
                       ( _player.SkillInfo.GetState ( SkillName.Alt   ) == SkillState.Using );

      return SkillFlag;
    }

    private bool ShouldTransitToWalk() {
      bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
      bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
      bool WalkFlag         = OnlyLeftKeyDown || OnlyRightKeyDown;

      return _player.State.Ground && WalkFlag;
    }

    private bool ShouldTransitToStepDownJump() {
      return !_player.State.CanNotDownGround && _player.State.Ground && Input.GetButton("Jump");
    }

    private bool ShouldTransitToIdle() {
      bool DownKeyUp = Input.GetKeyUp(KeyCode.DownArrow);
      bool UpKeyDown = Input.GetKeyDown(KeyCode.UpArrow);

      return _player.State.Ground && (DownKeyUp || UpKeyDown);
    }

    private BattlePlayer _player;
  }
}

