﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(SkillSynchronizer))]
  public class WarriorShift : Skill {
    void Awake() {
      _synchronizer = GetComponent<SkillSynchronizer>();
      _hitRistrictor = new HitRistrictor(_hitInfo);

      _renderer = GetComponent<SpriteRenderer>();
      _renderer.enabled = false;
      _animator = GetComponent<Animator>();
      _animator.enabled = false;
      _collider = GetComponent<Collider2D>();
      _collider.enabled = false;
      _photonView = GetComponent<PhotonView>();

      _instantiatedTime = Time.time;

      this.UpdateAsObservable()
        .Where(_ => Time.time - _instantiatedTime > _collisionOccurenceTime)
        .Take(1)
        .Subscribe(_ => {
          _renderer.enabled = true;
          _animator.enabled = true;
          _collider.enabled = true;
        })
        .AddTo(this);

      this.UpdateAsObservable()
        .Where(_ => _photonView.isMine )
        .Where(_ => Time.time - _instantiatedTime > _secondInstantiateTime)
        .Take(1)
        .Subscribe(_ => {
          var secondSkill = PhotonNetwork.Instantiate(_secondPath, transform.position, transform.rotation, 0).GetComponent<Skill>();
          secondSkill.SyncInit(_skillUserViewID);

          SkillReference.Instance.Register(secondSkill.viewID);
        })
        .AddTo(this);
    }

    void OnTriggerEnter2D(Collider2D collider) {
      if (PhotonNetwork.isMasterClient) {
        var target = collider.gameObject.GetComponent<IPhoton>();

        if (target == null)
          return;
        if (TeamChecker.IsSameTeam(collider.gameObject, _skillUserObj))
          return;
        if (_hitRistrictor.ShouldRistrict(collider.gameObject))
          return;

        DamageCalculator.Calculate(_skillUserObj, _attackInfo);

        _synchronizer.SyncAttack(_skillUserViewID, target.PhotonView.viewID, DamageCalculator.Damage, DamageCalculator.IsCritical, HitEffectType.Warrior);
      }
    }

    [SerializeField] private AttackInfo _attackInfo;
    [SerializeField] private HitInfo _hitInfo;

    private SkillSynchronizer _synchronizer;
    private HitRistrictor _hitRistrictor;

    private SpriteRenderer _renderer;
    private Animator _animator;
    private Collider2D _collider;
    private PhotonView _photonView;
    private float _instantiatedTime;
    private float _collisionOccurenceTime = 0.3f;
    private float _secondInstantiateTime = 0.8f;

    private string _secondPath = "Prefabs/Skill/Warrior/Shift2";
  }
}

