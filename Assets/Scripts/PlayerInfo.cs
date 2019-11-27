﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerInfo : NetworkBehaviour
{
    public int TurnNumber;
    public string Name;
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
            CmdSetPlayerName(_yourNameInput.text);
            _playerNameCanvas.SetActive(false);
            _yourNameInput.text = "";
        }
    }

    /// <summary>
    /// lol
    /// </summary>
    /// <param name="name"></param>
    public void ActuallySetName(string name)
    {
        Name = name;
        DisplayText.text = name;
    }

    [Command]
    private void CmdSetPlayerName(string name)
    {
        _gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        _gm.RpcSetPlayerName(TurnNumber, name);
    }
}
