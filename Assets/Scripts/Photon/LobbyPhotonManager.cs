﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyPhotonManager : Photon.PunBehaviour {
  void Start() {
    var currentScene = SceneManager.GetSceneByName("Lobby");
    MonoUtility.Instance.DelayUntil(() => currentScene == SceneManager.GetActiveScene(), () => {
      PhotonNetwork.Instantiate("Prehabs/Player", new Vector3(0, 0, 0), Quaternion.identity, 0);
    });
  }

  public override void OnPhotonPlayerConnected(PhotonPlayer other) {
    Debug.Log("OnPhotonPlayerConnected() was called" + other.NickName);
  }

  public override void OnPhotonPlayerDisconnected(PhotonPlayer other) {
    Debug.Log("OnPhotonPlayerDisconnected() was called" + other.NickName);
  }

  /*
  public override void OnLeftRoom() {
    Debug.Log("OnLeftRoom() was called");
    _sceneChanger.ChangeScene(_nextSceneName);
  }
  */

  public void Apply() {
    Debug.Log("Apply() was called");
    _nextSceneName = "Battle";
  }

  public void Logout() {
    Debug.Log("Logout() was called");
    _nextSceneName = "Registration";
    PhotonNetwork.LeaveRoom();
  }

  [SerializeField] private SceneChanger _sceneChanger;
  private string _nextSceneName;
}

