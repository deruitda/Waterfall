using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerInfo : NetworkBehaviour
{

    [SyncVar]
    public int TurnNumber;
    [SyncVar]
    public string Name;
    [SyncVar]
    public string PlayerID;
    public Text DisplayText;
    public InputField _yourNameInput;
    public GameObject _playerNameCanvas;
    private GameMaster _gm;

    public void Start()
    {
        DisplayText.text = Name;
    }

    public void SetName()
    {
        _yourNameInput = GameObject.Find("YourNameInput").GetComponent<InputField>();
        _playerNameCanvas = GameObject.Find("YourNameCanvas");

        if (!string.IsNullOrEmpty(_yourNameInput.text))
        {
            Name = _yourNameInput.text;
            DisplayText.text = Name;
            _yourNameInput.text = "";
            CmdNameChanged(Name);

            Cursor.visible = false;
            _playerNameCanvas.SetActive(false);
        }
    }

    [Command]
    public void CmdNameChanged(string name)
    {
        this.Name = name;
        this.DisplayText.text = name;
        RpcNameChanged(name);
    }

    [ClientRpc]
    public void RpcNameChanged(string name)
    {
        this.Name = name;
        this.DisplayText.text = name;
    }

    /// <summary>
    /// sync the state of this player with what came from the server
    /// </summary>
    /// <param name="json"></param>
    //public void SyncState(PlayerInfoStruct infoStruct)
    //{
    //    this.Info.TurnNumber = infoStruct.TurnNumber;
    //    this.Info.Name = infoStruct.Name;
    //}

}
