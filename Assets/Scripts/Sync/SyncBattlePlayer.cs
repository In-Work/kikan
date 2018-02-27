﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class SyncBattlePlayer : MonoBehaviour {
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
      if (stream.isWriting) {
        stream.SendNext(_renderer.flipX);
        stream.SendNext(_anim.GetBool("Idle"       ));
        stream.SendNext(_anim.GetBool("Fall"       ));
        stream.SendNext(_anim.GetBool("Walk"       ));
        stream.SendNext(_anim.GetBool("Ladder"     ));
        stream.SendNext(_anim.GetBool("LieDown"    ));
        stream.SendNext(_anim.GetBool("GroundJump" ));
        stream.SendNext(_anim.GetBool("LadderJump" ));
        stream.SendNext(_anim.GetBool("Skill"      ));
        stream.SendNext(_anim.GetBool("Die"        ));
        stream.SendNext(_anim.GetBool("Stun"       ));
      } else {
        _renderer.flipX = (bool)stream.ReceiveNext();
        _anim.SetBool("Idle"       , (bool)stream.ReceiveNext());
        _anim.SetBool("Fall"       , (bool)stream.ReceiveNext());
        _anim.SetBool("Walk"       , (bool)stream.ReceiveNext());
        _anim.SetBool("Ladder"     , (bool)stream.ReceiveNext());
        _anim.SetBool("LieDown"    , (bool)stream.ReceiveNext());
        _anim.SetBool("GroundJump" , (bool)stream.ReceiveNext());
        _anim.SetBool("LadderJump" , (bool)stream.ReceiveNext());
        _anim.SetBool("Skill"      , (bool)stream.ReceiveNext());
        _anim.SetBool("Die"        , (bool)stream.ReceiveNext());
        _anim.SetBool("Stun"       , (bool)stream.ReceiveNext());
      }
    }

    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Animator _anim;
  }
}
