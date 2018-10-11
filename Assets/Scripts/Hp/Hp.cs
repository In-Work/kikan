﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class Hp : Gauge<int> {
    public Hp(List<IObserver> observerList) {
      Notifier = new Notifier(observerList);
    }

    public override void Add(int quantity) {
      Cur += quantity;
      AdjustBoundary();

      Notifier.Notify(Notification.HpAdd, Cur, Max);
    }

    public override void Subtract(int quantity) {
      Cur -= quantity;
      AdjustBoundary();

      Notifier.Notify(Notification.HpSubtract, Cur, Max);
    }

    private void AdjustBoundary() {
      if (Cur < Min)
        Cur = Min;
      if (Cur > Max)
        Cur = Max;
    }

    public Notifier Notifier { get; private set; }

    public void UpdateView() {}
    public void FullRecover() {}
    public void AttachHudBar(Bar hudBar) {}
    public void UpdateMaxHp() {}
    public void ForceSync(int cur, int max) {}
    public void ForceSyncCur(int cur) {}
    public void ForceSyncMax(int max) {}
    public void ForceSyncUpdateView() {}
  }
}

