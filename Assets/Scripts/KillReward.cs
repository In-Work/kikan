﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillReward : MonoBehaviour {
  public int Exp {
    get {
      return _expTable.Data[_level.Lv - 1];
    }
  }

  public int Gold {
    get {
      return _goldTable.Data[_level.Lv - 1];
    }
  }

  [SerializeField] private DataTable _expTable;
  [SerializeField] private DataTable _goldTable;
  [SerializeField] private Level _level;
}

