using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class JoinGame : MonoBehaviour
{
    private NetworkManager _networkManager;
    private List<GameObject> _roomList;
    [SerializeField]
    private Text _status;
    [SerializeField]
    private GameObject _roomListItemPrefab;
    [SerializeField]
    private Transform _roomListParent;
    // Start is called before the first frame update
    void Start()
    {
        _roomList = new List<GameObject>();
        _networkManager = NetworkManager.singleton;

        if (_networkManager.matchMaker == null)
            _networkManager.StartMatchMaker();

        RefreshRoomList();
    }

    public void RefreshRoomList()
    {
        ClearRoomList();
        _networkManager.matchMaker.ListMatches(0, 20, "", false, 0, 0, OnMatchList);
        _status.text = "Loading matches...";
    }

    public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {
        _status.text = "";

        if (matches == null)
        {
            _status.text = "Could not load rooms!!!!!!!!!!";
            return;
        }
        else if(matches.Count == 0)
        {
            _status.text = "No active rooms available";
            return;
        }

        ClearRoomList();

        foreach(MatchInfoSnapshot match in matches)
        {
            //add the room button object and parent it to the scrollview
            GameObject roomItemGameObject = Instantiate(_roomListItemPrefab);
            roomItemGameObject.transform.SetParent(_roomListParent);

            RoomListItem roomListItem = roomItemGameObject.GetComponent<RoomListItem>();
            if(roomListItem != null)
            {
                roomListItem.Setup(match, JoinRoom);
            }

            _roomList.Add(roomItemGameObject);
        }
    }

    private void ClearRoomList()
    {
        for (int i = 0; i < _roomList.Count; i++)
        {
            Destroy(_roomList[i]);
        }

        _roomList.Clear();
    }

    public void JoinRoom(MatchInfoSnapshot match)
    {
        _status.text = $"Joining match {match.name}";
        Debug.Log($"Joining match: {match.name}");
        _networkManager.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, _networkManager.OnMatchJoined);
    }
}
