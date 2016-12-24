﻿using UnityEngine;
using System.Collections;

public class HealthSystem : MonoBehaviour {
  public void Init(Health health, Bar bar) {
    _health = health;
    _bar = bar;
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
  private Health _health;
}

