﻿using UnityEngine;
using System;
using System.Collections;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(Character2D))]
  [RequireComponent(typeof(EnemyObserver))]
  public class Enemy : MonoBehaviour, ICharacter, IBattle {
    void Awake() {
      Mediator      = new EnemyMediator(this);
      State         = new CharacterState(_ladderCollider, _footCollider);
      BuffState     = new BuffState(Observer);
      StateTransfer = new StateTransfer(_initState, _animator);
      Hp            = new EnemyHp();

      Hp.Notifier.Add(_hpBar.OnNotify);

      Mediator.Notifier.Add(Hp.OnNotify);
      Mediator.Notifier.Add(NumberPopupEnvironment.Instance.OnNotify);
      Mediator.Notifier.Add(KillRewardEnvironment.Instance.OnNotify);

      var notifier = new Notifier(Mediator.OnNotify);
      notifier.Notify(Notification.EnemyInstantiated);
    }

    public void AttachPopulationObserver(EnemyPopulationObserver populationObserver) {
      PopulationObserver = populationObserver;
    }

    // Unity
    public PhotonView     PhotonView   { get { return _photonView;     } }
    public Transform      Transform    { get { return transform;       } }
    public SpriteRenderer Renderer     { get { return _spriteRenderer; } }
    public Rigidbody2D    Rigid        { get { return _rigid;          } }
    public Collider2D     BodyCollider { get { return _bodyCollider;   } }
    public Collider2D     FootCollider { get { return _footCollider;   } }

    // Observer
    public EnemyObserver           Observer           { get { return _observer; } }
    public EnemyPopulationObserver PopulationObserver { get; private set; }

    public IResponder Mediator { get; private set; }

    // tmp
    public MonoBehaviour AI { get { return _ai; } }

    public EnemyData Data { get { return _enemyData; } }

    // Enemy
    public CharacterState State         { get; private set; }
    public BuffState      BuffState     { get; private set; }
    public StateTransfer  StateTransfer { get; private set; }
    public EnemyHp        Hp            { get; private set; }

    public int KillExp  { get { return _killExp;  } }
    public int KillGold { get { return _killGold; } }
    public int DamageSkinId { get { return 0; } }

    [Header("Unity/Photon Components")]
    [SerializeField] private PhotonView     _photonView;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D    _rigid;
    [SerializeField] private Collider2D     _bodyCollider;
    [SerializeField] private Collider2D     _ladderCollider;
    [SerializeField] private Collider2D     _footCollider;
    [SerializeField] private Animator       _animator;

    [Header("Observer")]
    [SerializeField] private EnemyObserver _observer;

    [Header("Kill Reward")]
    [SerializeField] private int _killExp;
    [SerializeField] private int _killGold;

    // tmp
    [Space(10)]
    [SerializeField] private MonoBehaviour _ai;

    [Header("Hp")]
    [SerializeField] private Bar _hpBar;

    [Header("Data")]
    [SerializeField] private EnemyData _enemyData;

    private static readonly string _initState = "Idle";
  }
}

