﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface ICharacter : IPhoton {
    Transform      transform      { get; }
    Rigidbody2D    Rigid          { get; }
    Collider2D     BodyCollider   { get; }
    Collider2D     FootCollider   { get; }
    CharacterState State          { get; }
    LocationJudger LocationJudger { get; }
  }
}

