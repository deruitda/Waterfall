using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HostGame : MonoBehaviour
{
    [SerializeField]
    private uint _roomSize = 8;
    private string _roomName;
    private NetworkManager _networkManager;

    public void Start()
    {
        _networkManager = NetworkManager.singleton;

        if (_networkManager.matchMaker == null)
            _networkManager.StartMatchMaker();


    }

    public void SetRoomName(string roomName)
    {
        _roomName = roomName;
    }

    public void CreateRoom()
    {
        if (!string.IsNullOrEmpty(_roomName))
        {
            Debug.Log($"Creating room {_roomName}...");

            _networkManager.matchMaker.CreateMatch(_roomName, _roomSize, true, "", "", "", 0, 0, _networkManager.OnMatchCreate);
        }
    }
}
