﻿using UnityEngine;
using System.Collections;

public class GroundJumpSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_photonView == null) {
      _photonView = animator.GetComponent<PhotonView>();
      _rigidState = animator.GetComponent<RigidState>();
      _jump = animator.GetComponent<GroundJump>();
    }

    Debug.Log("jump");
    _jump.Jump();
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_photonView.isMine) {
      bool SkillFlag = Input.GetKey(KeyCode.X) ||
                       Input.GetKey(KeyCode.LeftShift) ||
                       Input.GetKey(KeyCode.Z);

      if (SkillFlag) { ActTransition("Skill", animator); return; }

      if (_rigidState.Air) {
        ActTransition("Fall", animator); return;
      }
    }
  }

  private void ActTransition(string stateName, Animator animator) {
    animator.SetBool(stateName, true);
    animator.SetBool("GroundJump", false);
  }

  private PhotonView _photonView;
  private RigidState _rigidState;
  private GroundJump _jump;
}

