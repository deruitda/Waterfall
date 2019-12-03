using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static PlayerInfo;
using Newtonsoft.Json;

public class GameMaster : NetworkBehaviour
{
    public List<Material> availableMats; //< Never change, this stores default materials
    public List<Material> playingMats; //< Used in the game, reset with availableMats
    public PlayingCard _currentSelectedCard;
    public BuschLiteCan _can;
    private CustomNetworkManager _networkManager;
    public Text TurnText;
    [SyncVar]
    private int _currentTurn;
    public int CurrentTurn { get { return _currentTurn; } private set { _currentTurn = value; } }
    public List<PlayerInfo> Players = new List<PlayerInfo>();

    // Start is called before the first frame update
    void Start()
    {
        CopyCards(); //< TODO: Need to clone, not set equal to
        Cursor.visible = false;
        _networkManager = NetworkManager.singleton.GetComponent<CustomNetworkManager>();
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        Debug.Log("New game started!");
        CopyCards();
        _can.NewCan();
    }

    public void SelectCard(PlayingCard card, int turnNumber)
    {
        //if it's not the callers turn, return
        if (turnNumber != CurrentTurn)
        {
            Debug.Log($"It's not player {turnNumber}'s turn!");
            return;
        }

        _currentSelectedCard = card;
        int rand = UnityEngine.Random.Range(0, playingMats.Count - 1);

        _can.placeCard();
        card.RpcSetMaterial(rand);
        RpcRemoveMatFromAvailable(rand);

        SetCurrentTurn();
    }

    private void SetCurrentTurn()
    {
        //if we are at the max number of turns, reset
        if (_networkManager.PlayerCount - 1 == CurrentTurn && _networkManager.PlayerCount != 0)
            CurrentTurn = 0;
        else
            CurrentTurn++;
        string text = $"It is now player {CurrentTurn}'s turn"; 
        Debug.Log(text);
        RpcSetTurnText(text);
    }

    [ClientRpc]
    void RpcSetTurnText(string text)
    {
        TurnText.text = text;
    }

    [ClientRpc]
    public void RpcRemoveMatFromAvailable(int index)
    {
        playingMats.RemoveAt(index);

        if (playingMats.Count == 1)
        {
            Debug.Log("Last card of the deck");
        }

        if (playingMats.Count <= 0)
        {
            Debug.Log("Last card has been drawn. Drawing another card will start a new game."); //< TODO: replace with New Game interface
            NewGame();
        }
    }

    public Material GetMat(int index)
    {
        Material mat = playingMats[index];
        return mat;
    }

    private void CopyCards()
    {
        for (int i = 0; i < availableMats.Count; i++)
        {
            playingMats.Add(availableMats[i]);
        }
    }

    /// <summary>
    /// send list of players to the clients
    /// </summary>
    [Command]
    public void CmdSyncPlayerStates()
    {
        List<PlayerInfoStruct> playerInfoStructs = new List<PlayerInfoStruct>();
        foreach (PlayerInfo playerInfo in Players)
            playerInfoStructs.Add(playerInfo.Info);

        string json = JsonConvert.SerializeObject(playerInfoStructs);
        
        Debug.Log(json);
        RpcSyncPlayerStates(json);
    }

    [ClientRpc]
    public void RpcSyncPlayerStates(string json)
    {
        //sync up the list of players
        Players = GameObject.FindObjectsOfType<PlayerInfo>().ToList();

        List<PlayerInfoStruct> playerInfoStructs = JsonConvert.DeserializeObject<List<PlayerInfoStruct>>(json);
        foreach (PlayerInfo player in Players)
        {
            PlayerInfoStruct info = playerInfoStructs.First(i => i.PlayerID == player.Info.PlayerID);
            //send the json to the client's players
            player.SyncState(info);
        }
    }
}
