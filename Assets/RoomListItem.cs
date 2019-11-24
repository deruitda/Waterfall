using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class RoomListItem : MonoBehaviour
{
    [SerializeField]
    private Text _roomNameText;
    public delegate void JoinRoomDelegate(MatchInfoSnapshot match);
    public JoinRoomDelegate _joinRoomCallback;

    private MatchInfoSnapshot _match;

    public void Setup(MatchInfoSnapshot match, JoinRoomDelegate callback)
    {
        _match = match;
        _joinRoomCallback = callback;
        _roomNameText.text = $"{match.name} ({match.currentSize}/{match.maxSize})";
    }

    public void JoinGame()
    {
        _joinRoomCallback.Invoke(_match);
    }
}
