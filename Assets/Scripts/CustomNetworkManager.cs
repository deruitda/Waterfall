﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;

public class CustomNetworkManager : NetworkManager
{
    public int PlayerCount { get; private set; } = 0;

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        var player = (GameObject)GameObject.Instantiate(playerPrefab, new Vector3(9.65f, 2f, -14.28f), Quaternion.identity);
        PlayerInfo playerInfo = player.GetComponent<PlayerInfo>();

        //add the player to our list and increment the count
        GameMaster _gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        _gm.RpcAddPlayer(playerInfo);
        playerInfo.TurnNumber = PlayerCount++;

        NetworkServer.AddPlayerForConnection(conn, player, (short)PlayerCount);
    }
}
