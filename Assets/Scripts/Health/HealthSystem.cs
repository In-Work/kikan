﻿using UnityEngine;
using System.Collections;

public class HealthSystem : MonoBehaviour {
  void Awake() {
    _bar = Instantiate(_bar) as Bar;

    var canvas = GameObject.Find(_canvasName);
    _bar.transform.SetParent(canvas.transform, false);
  }

  public void Init(Health health) {
    _health = health;
  }

  public void IsHealed(int quantity) {
    _health.Plus(quantity);
    Show();
  }

  public void IsDamaged(int quantity) {
    IsHealed(-quantity);

    if (_health.Dead)
      Die();
  }

  public void Show() {
    _bar.Show(_health.Cur, _health.Max);
  }

  public void Die() {
    _anim.SetBool("Die", true);
  }

  [SerializeField] private Bar _bar;
  [SerializeField] private Animator _anim;
  [SerializeField] private string _canvasName;
  private Health _health;
}

