﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class NumberEffect {
    /*
    public NumberEffect(GameObject digitPref) {
      _digitPref = digitPref;
    }

    public void PopupHit(int number, DamageSkin skin) {
      Popup(number, skin, DamageType.Hit);
    }

    public void PopupCritical(int number, DamageSkin skin) {
      Popup(number, skin, DamageType.Critical);
    }

    public void PopupTake(int number, DamageSkin skin) {
      Popup(number, skin, DamageType.Take);
    }

    public void PopupHeal(int number, DamageSkin skin) {
      Popup(number, skin, DamageType.Heal);
    }

    private void Popup(int number, DamageSkin skin, DamageType type) {
      Number = number;

      // INFO: e.g. number = 8351 -> "8351" -> ['8','3','5','1'] -> indices = [8, 3, 5, 1]
      var indices = number.ToString().ToCharArray().Select(x => Convert.ToInt32(x.ToString()));

      int i = 0;
      foreach(int index in indices) {
        var digit = Instantiate(_digitPref, gameObject.transform, false);
        digit.transform.Translate(i * 0.3f, 1.0f, 0.0f);
        ++i;

        var renderer = digit.GetComponent<SpriteRenderer>();

        switch (type) {
          case DamageType.Hit:
            renderer.sprite = skin.Hit[index];
            break;
          case DamageType.Critical:
            renderer.sprite = skin.Critical[index];
            break;
          case DamageType.Take:
            renderer.sprite = skin.Take[index];
            break;
          case DamageType.Heal:
            renderer.sprite = skin.Heal[index];
            break;
        }

        renderer.sortingOrder = i;
      }
    }

    public int Number { get; private set; }
    private GameObject _digitPref;
    */
  }
}

