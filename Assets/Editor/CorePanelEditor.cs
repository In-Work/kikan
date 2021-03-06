﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Bunashibu.Kikan {
  [CustomEditor(typeof(CorePanel))]
  public class CorePanelEditor : Editor {
    public override void OnInspectorGUI() {
      base.OnInspectorGUI();

      string message = "The order of cores must be below\n" +
                       "\n"+
                       "0, Speed    (Core)\n" +
                       "1, Hp       (Core)\n" +
                       "2, Attack   (Core)\n" +
                       "3, Critical (Core)\n" +
                       "4, Heal     (Core)\n";

      EditorGUILayout.HelpBox(message, MessageType.Info);
    }
  }
}

