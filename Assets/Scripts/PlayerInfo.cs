using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerInfo : NetworkBehaviour
{
    public int TurnNumber;

    [SyncVar]
    public string Name;
    public Text DisplayText;
    public InputField _yourNameInput;
    public GameObject _playerNameCanvas;

    public void Update()
    {
        if (!DisplayText.text.Equals(Name))
        {
            DisplayText.text = Name;
        }
    }

    public void SetName()
    {
        _yourNameInput = GameObject.Find("YourNameInput").GetComponent<InputField>();
        _playerNameCanvas = GameObject.Find("YourNameCanvas");

        if (!string.IsNullOrEmpty(_yourNameInput.text))
        {
            Name = _yourNameInput.text;
            _playerNameCanvas.SetActive(false);
            _yourNameInput.text = "";
        }
    }
}
