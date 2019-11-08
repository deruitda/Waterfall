using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameMaster : NetworkBehaviour
{
    public List<Material> availableMats; //< Never change, this stores default materials
    public List<Material> playingMats; //< Used in the game, reset with availableMats
    public PlayingCard _currentSelectedCard;
    public BuschLiteCan _can;
    public CustomNetworkManager _networkManager;
    [SyncVar]
    private int _currentTurn;
    public int CurrentTurn { get { return _currentTurn; } private set { _currentTurn = value; } }

    // Start is called before the first frame update
    void Start()
    {
        CopyCards(); //< TODO: Need to clone, not set equal to
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

        Debug.Log($"It is now {CurrentTurn}'s turn");
    }

    [ClientRpc]
    public void RpcRemoveMatFromAvailable(int index)
    {
        playingMats.RemoveAt(index);
        _can.placeCard();

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
}
