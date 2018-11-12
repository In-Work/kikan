﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public class Hp : IntReactiveGauge {
    public Hp(int initialHp) {
      _cur.Value = initialHp;
      _min.Value = 0;
      _max.Value = initialHp;
    }

    public void FullRecover() {
      _cur.Value = _max.Value;
    }
  }
}

