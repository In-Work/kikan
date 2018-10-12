﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class HpBar : Bar {
    public override void OnNotify(Notification notification, object[] args) {
      switch (notification) {
        case Notification.HpUpdated:
          _view.UpdateView((int)args[0], (int)args[1]); // cur, max
          break;
        default:
          break;
      }
    }
  }
}

