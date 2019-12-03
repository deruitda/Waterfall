using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerInfo : NetworkBehaviour
{
    public PlayerInfoStruct Info;

    public Text DisplayText;
    public InputField _yourNameInput;
    public GameObject _playerNameCanvas;
    private GameMaster _gm;

    public void Start()
    {

    }

    public void SetName()
    {
        _yourNameInput = GameObject.Find("YourNameInput").GetComponent<InputField>();
        _playerNameCanvas = GameObject.Find("YourNameCanvas");

        if (!string.IsNullOrEmpty(_yourNameInput.text))
        {
            _playerNameCanvas.SetActive(false);
            _yourNameInput.text = "";
        }
    }

    /// <summary>
    /// sync the state of this player with what came from the server
    /// </summary>
    /// <param name="json"></param>
    public void SyncState(PlayerInfoStruct infoStruct)
    {
        this.Info.TurnNumber = infoStruct.TurnNumber;
        this.Info.Name = infoStruct.Name;
    }

    public struct PlayerInfoStruct {
        public int TurnNumber;
        public string Name;
        public string PlayerID;
    }

}
