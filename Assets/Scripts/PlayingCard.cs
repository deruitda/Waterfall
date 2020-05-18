using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayingCard : NetworkBehaviour
{
    public MeshRenderer _meshRenderer;
    private Material _newMat;
    public GameMaster _gm;
    public BuschLiteCan _can;
    public PlayingCardType CardType;

    private void Start()
    {
        CardType = PlayingCardType.NoVal;
    }

    public void Select(int playerTurn)
    {
        _gm.SelectCard(this, playerTurn);
    }

    [ClientRpc]
    public void RpcSetMaterial(int rand)
    {
        _meshRenderer.material = _gm.GetMat(rand);
        ParseCardValue();
    }

    /// <summary>
    /// Temporary function for determining the card value. 
    /// </summary>
    private void ParseCardValue()
    {
        string matName = _meshRenderer.material.name;

        string[] matNameChunks = matName.Split('_'); //split the name out
        string cardName = matNameChunks[matNameChunks.Length - 2]; //get the card name. Formatted as Heart02, Spade09, Diamond13 etc

        int cardVal = Int32.Parse(cardName.Substring(cardName.Length - 2, 2)); //parse the numeric value

        this.CardType = (PlayingCardType) cardVal;
    }

    public enum PlayingCardType
    {
        NoVal = 0,
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12, 
        King = 13
    }
}
