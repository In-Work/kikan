﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class Weapon : MonoBehaviour {
    void Awake() {
      Stream = new WeaponStream();

      _instantiator = new SkillInstantiator(this, _player);
      _ctManager = new SkillCTManager(this, _player);
    }

    public bool IsUsable(int i) {
      return _ctManager.IsUsable(i);
    }

    public void EnableInstantiate() {
      CanInstantiate = true;
    }

    public void DisableInstantiate() {
      CanInstantiate = false;
    }

    public void ResetAllCT() {
      _ctManager.ResetAllCT();

      _player.State.Rigor = false;
      for (int i=0; i<_keysList.Count; ++i)
        _player.SkillInfo.SetState(_skillNames[i], SkillState.Ready);
    }

    public WeaponStream Stream { get; private set; }

    public string        JobName      => _jobName;
    public List<KeyList> KeysList     => _keysList;
    public SkillName[]   SkillNames   => _skillNames;
    public int[]         RequireLv    => _requireLv;
    public Vector3[]     AppearOffset => _appearOffset;
    public float[]       SkillCT      => _skillCT;

    public bool CanInstantiate = true;

    [SerializeField] private Player _player;
    [SerializeField] private string _jobName;
    [SerializeField] private List<KeyList> _keysList;
    [SerializeField] private SkillName[] _skillNames;
    [SerializeField] private int[] _requireLv;
    [SerializeField] private Vector3[] _appearOffset;
    [SerializeField] private float[] _skillCT;
    [SerializeField] private float[] _rigorCT;

    private SkillInstantiator _instantiator;
    private SkillCTManager _ctManager;

    /*
      if (_isDisabled[i] && (_player.Level.Cur.Value == _requireLv[i]))
        EnableSkill(i);
    */

    /*
    private void EnableSkill(int index) {
      var preSizeDelta = _panelUnitList[index].AlphaRectTransform.sizeDelta;
      _panelUnitList[index].AlphaRectTransform.sizeDelta = new Vector2(preSizeDelta.x, 0);
      _isDisabled[index] = false;
    }
    */
  }

  [System.SerializableAttribute]
  public class KeyList {
    public List<KeyCode> keys;
  }
}

