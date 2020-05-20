using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class BlackBoardTextEditor : NetworkBehaviour
{
    public TextMeshPro BlackBoardText;

    void Start()
    {
        if (BlackBoardText == null)
            BlackBoardText = this.gameObject.GetComponent<TextMeshPro>();
    }

    public void SetBoardText(string text)
    {
        BlackBoardText.text = text;

        if (isServer)
            RpcSetBoardText(text);
    }

    [ClientRpc]
    private void RpcSetBoardText(string text)
    {
        BlackBoardText.text = text;
    }
}
