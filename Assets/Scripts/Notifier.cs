﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class Notifier {
    public Notifier(List<IObserver> observerList) {
      _observerList = new List<IObserver>();

      foreach (IObserver observer in observerList)
        Add(observer);
    }

    public void Notify(Notification notification, params object[] args) {
      foreach (IObserver observer in _observerList)
        observer.OnNotify(notification, args);
    }

    public void Add(IObserver observer) {
      _observerList.Add(observer);
    }

    private List<IObserver> _observerList;
  }
}

