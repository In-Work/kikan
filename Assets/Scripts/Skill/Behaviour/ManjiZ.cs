﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManjiZ : Skill {
  void OnTriggerEnter2D(Collider2D collider) {
    if (PhotonNetwork.isMasterClient) {
      /*
      var target = collider.gameObject;
      var targetUser = target.GetComponent<PhotonView>().owner;

      if (targetUser != _skillUser) {
        if (target.tag == "Player") {
          var health = target.GetComponent<PlayerHealth>();
          health.Minus(30);
          health.Show();
        }
      }
      */
    }
  }

  [SerializeField] private BoxCollider2D _collider;
}

