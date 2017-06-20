﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyWalkSMB : StateMachineBehaviour {
  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_player.PhotonView.isMine) {
      GroundMove();

      if ( ShouldTransitToSkill()        ) { _player.StateTransfer.TransitTo( "Skill"        , animator ); return; }
      if ( ShouldTransitToClimb()        ) { _player.StateTransfer.TransitTo( "Climb"        , animator ); return; }
      if ( ShouldTransitToStepDownJump() ) { _player.StateTransfer.TransitTo( "StepDownJump" , animator ); return; }
      if ( ShouldTransitToGroundJump()   ) { _player.StateTransfer.TransitTo( "GroundJump"   , animator ); return; }
      if ( ShouldTransitToIdle()         ) { _player.StateTransfer.TransitTo( "Idle"         , animator ); return; }
      if ( _player.RigidState.Air        ) { _player.StateTransfer.TransitTo( "Fall"         , animator ); return; }
    }
  }

  private void GroundMove() {
    bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
    bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);

    if (OnlyLeftKeyDown)  {
      _player.Movement.GroundMoveLeft();

      foreach (var sprite in _player.Renderers)
        sprite.flipX = false;
    }

    if (OnlyRightKeyDown) {
      _player.Movement.GroundMoveRight();

      foreach (var sprite in _player.Renderers)
        sprite.flipX = true;
    }
  }

  private bool ShouldTransitToSkill() {
    bool SkillFlag = ( _player.SkillInfo.GetState ( SkillName.X     ) == SkillState.Using ) ||
                     ( _player.SkillInfo.GetState ( SkillName.Shift ) == SkillState.Using ) ||
                     ( _player.SkillInfo.GetState ( SkillName.Z     ) == SkillState.Using );

    return SkillFlag;
  }

  private bool ShouldTransitToClimb() {
    bool OnlyUpKeyDown   = Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow);
    bool OnlyDownKeyDown = Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow);
    bool ClimbFlag       = ( OnlyUpKeyDown   && !_player.RigidState.LadderTopEdge    ) ||
                           ( OnlyDownKeyDown && !_player.RigidState.LadderBottomEdge );

    return _player.RigidState.Ladder && ClimbFlag;
  }

  private bool ShouldTransitToStepDownJump() {
    bool OnlyDownKeyDown  = Input.GetKey(KeyCode.DownArrow)  && !Input.GetKey(KeyCode.UpArrow);

    return _player.RigidState.Ground && OnlyDownKeyDown && Input.GetButton("Jump");
  }

  private bool ShouldTransitToGroundJump() {
    return _player.RigidState.Ground && Input.GetButton("Jump");
  }

  private bool ShouldTransitToIdle() {
    bool BothKeyDown = Input.GetKey(KeyCode.LeftArrow)   && Input.GetKey(KeyCode.RightArrow);
    bool OneKeyUp    = Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow);

    return _player.RigidState.Ground && (BothKeyDown || OneKeyUp);
  }

  [SerializeField] private LobbyPlayerSMB _player;
}

