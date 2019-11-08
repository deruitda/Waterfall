﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetworkManager : NetworkManager
{
    public int PlayerCount { get; private set; } = 0;
    public List<PlayerInfo> Players = new List<PlayerInfo>();

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        var player = (GameObject)GameObject.Instantiate(playerPrefab, new Vector3(9.65f, 3.36f, -14.28f), Quaternion.identity);
        PlayerInfo playerInfo = player.GetComponent<PlayerInfo>();

        //add the player to our list and increment the count
        Players.Add(playerInfo);
        playerInfo.TurnNumber = PlayerCount++;

        NetworkServer.AddPlayerForConnection(conn, player, (short)PlayerCount);
    }
}
