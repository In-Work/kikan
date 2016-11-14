﻿using UnityEngine;
using System;
using System.Collections;

public class Skill : MonoBehaviour {
  void Update() {
    UpdateFlag();
  }

  public void Activate() {
    if (!_flag) return;

    _canUse = false;
    _flag = false;
    _anim.SetBool(_name, true);

    StartCoroutine(DelayOneFrame(() => {
      _anim.SetBool(_name, false);
    }));

    StartCoroutine(DelaySec(_ct, () => {
      _canUse = true;
    }));
  }

  private void UpdateFlag() {
    if (_canUse && Input.GetKey(_key))
      _flag = true;
  }

  private IEnumerator DelayOneFrame(Action action) {
    yield return new WaitForEndOfFrame();
    action();
  }

  private IEnumerator DelaySec(float sec, Action action) {
    yield return new WaitForSeconds(sec);
    action();
  }

  [SerializeField] private KeyCode _key;
  [SerializeField] private Animator _anim;
  [SerializeField] private string _name;
  [SerializeField] private float _ct;
  private bool _canUse = true;
  public bool CanUse { get; private set; }
  private bool _flag;
}

