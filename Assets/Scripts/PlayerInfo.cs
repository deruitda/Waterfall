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
    public string PlayerID;
    public Text DisplayText;
    public InputField _yourNameInput;
    public GameObject _playerNameCanvas;
    private GameMaster _gm;

    private void Start()
    {
        DisplayText.text = $"Idiot #{TurnNumber}";
    }
}
