﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  // Obsolete
  public interface IPlayer {
    ReactiveProperty<int> KillCount  { get; }
    ReactiveProperty<int> DeathCount { get; }
  }
}

