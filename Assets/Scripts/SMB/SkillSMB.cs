﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_photonView == null) {
      _photonView = animator.GetComponent<PhotonView>();
      _rigidState = animator.GetComponent<RigidState>();
    }

    _transitionFlag = false;
    Debug.Log("skill");
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_photonView.isMine) {
      if (_rigidState.UsingSkill)
        _transitionFlag = true;

      if (_transitionFlag && !_rigidState.UsingSkill) {
        bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
        bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);

        if (_rigidState.Ground) {
          if (OnlyLeftKeyDown || OnlyRightKeyDown) { ActTransition("Walk", animator); return; }
                                                     ActTransition("Idle", animator); return;
        }

        if (_rigidState.Air) {
          ActTransition("Fall", animator); return;
        }
      }
    }
  }

  private void ActTransition(string stateName, Animator animator) {
    animator.SetBool(stateName, true);
    animator.SetBool("Skill", false);
  }

  private PhotonView _photonView;
  private RigidState _rigidState;
  private bool _transitionFlag;
}

