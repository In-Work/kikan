﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Bunashibu.Kikan {
  public class SkillPanel : MonoBehaviour {
    public virtual void Register(Weapon weapon) {
      weapon.Stream.OnCurCT
        .Subscribe(entity => {
          if (entity.Index == 0) // Ignore X alpha mask
            return;

          if (weapon.IsReachedToRequireLv(entity.Index))
            UpdateAlphaMask(entity.Index, entity.CurCT, weapon.SkillCT[entity.Index]);
        })
        .AddTo(weapon.gameObject);
    }

    private void UpdateAlphaMask(int i, float cur, float max) {
      if (max == 0)
        return;

      // NOTE: AlphaMask height == 55.0f
      var prevSizeDelta = _alphaRectTransform[i].sizeDelta;
      _alphaRectTransform[i].sizeDelta = new Vector2(prevSizeDelta.x, 55.0f * (cur / max));
    }

    [SerializeField] protected List<RectTransform> _alphaRectTransform;
  }
}

