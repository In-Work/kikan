﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class AudioEnvironment : MonoBehaviour {
    public void PlayOneShot(string key, float vol=1.0f) {
      int index = _keyList.IndexOf(key);
      _audioSource.PlayOneShot(_clipList[index], vol);
    }

    public void DisableListener() {
      _audioListener.enabled = false;
    }

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioListener _audioListener;
    [SerializeField] private List<AudioClip> _clipList;
    [SerializeField] private List<string> _keyList;
  }
}

