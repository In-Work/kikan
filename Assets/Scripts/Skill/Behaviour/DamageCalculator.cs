﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class DamageCalculator {
    public int CalculateDamage(int power, int maxDeviation, int criticalProbability) {
      int deviation = (int)((Random.value - 0.5) * 2 * maxDeviation);
      int damage = power + deviation;

      IsCritical = JudgeCritical(criticalProbability);
      if (IsCritical)
        damage *= 2;

      return damage;
    }

    private bool JudgeCritical(int probability) {
      int threshold = (int)(Random.value * 99);

      if (probability > threshold)
        return true;

      return false;
    }

    public bool IsCritical { get; private set; }
  }
}

