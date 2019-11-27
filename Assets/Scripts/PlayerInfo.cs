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


    public void Update()
    {
        if (!DisplayText.text.Equals(Name))
        {
            DisplayText.text = Name;
        }
    }
}
