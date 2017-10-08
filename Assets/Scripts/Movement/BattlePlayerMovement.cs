﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class BattlePlayerMovement {
    public BattlePlayerMovement() {
      _coreAirLinear    = new CoreAirLinearMove();
      _coreFlatMove     = new CoreFlatGroundMove();

      _coreGroundJump   = new CoreGroundJump();
      _coreClimbJump    = new CoreClimbJump();
      _stepDownJump     = new StepDownJump();

      _climb            = new Climb();
      _lieDown          = new LieDown();
    }

    // INFO: Must be called in MonoBehaviour-FixedUpdate()
    public void FixedUpdate(Rigidbody2D rigid, Transform trans) {
      _coreAirLinear.FixedUpdate(rigid);
      _coreFlatMove.FixedUpdate(rigid);

      _coreGroundJump.FixedUpdate(rigid);
      _coreClimbJump.FixedUpdate(rigid);
      _stepDownJump.FixedUpdate(rigid);

      _climb.FixedUpdate(trans);
    }

    public void AirMoveLeft() {
      _coreAirLinear.MoveLeft();
    }

    public void AirMoveRight() {
      _coreAirLinear.MoveRight();
    }

    public void GroundMoveLeft(float degAngle = 0) {
      _coreFlatMove.MoveLeft(degAngle);
    }

    public void GroundMoveRight(float degAngle = 0) {
      _coreFlatMove.MoveRight(degAngle);
    }

    public void GroundJump() {
      _coreGroundJump.Jump();
    }

    public void ClimbJump() {
      _coreClimbJump.JumpOff();
    }

    public void StepDownJump() {
      _stepDownJump.StepDown();
    }

    public void ClimbUp() {
      _climb.MoveUp();
    }

    public void ClimbDown() {
      _climb.MoveDown();
    }

    public void LieDown(BoxCollider2D collider) {
      _lieDown.Lie(collider);
    }

    public void Stand(BoxCollider2D collider) {
      _lieDown.Stand(collider);
    }

    public void SetLinearMoveForce(float force) {
      _coreFlatMove.SetForce(force);
    }

    public void SetJumpForce(float force) {
      _coreGroundJump.SetForce(force);
    }

    private CoreAirLinearMove _coreAirLinear;
    private CoreFlatGroundMove _coreFlatMove;

    private CoreGroundJump _coreGroundJump;
    private CoreClimbJump _coreClimbJump;
    private StepDownJump _stepDownJump;

    private Climb _climb;
    private LieDown _lieDown;

  }
}

