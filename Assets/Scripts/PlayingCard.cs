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

    [SerializeField]
    [SyncVar]
    private int cardTypeVal;

    public PlayingCardType CardType
    {
        get { return (PlayingCardType)cardTypeVal; }
    }

    private void Start()
    {
        cardTypeVal = 0;
    }

    public void Select(int playerTurn)
    {
        _gm.SelectCard(this, playerTurn);
    }

    public void SetMaterial(int rand)
    {
        _meshRenderer.material = _gm.GetMat(rand);
        SetCardType();

        RpcSetMaterial(rand);
    }

    [ClientRpc]
    private void RpcSetMaterial(int rand)
    {
        _meshRenderer.material = _gm.GetMat(rand);
    }

    /// <summary>
    /// Temporary function for determining the card value. 
    /// </summary>
    private void SetCardType()
    {
        string matName = _meshRenderer.material.name;

        string[] matNameChunks = matName.Split('_'); //split the name out
        string cardName = matNameChunks[matNameChunks.Length - 2]; //get the card name. Formatted as Heart02, Spade09, Diamond13 etc

        this.cardTypeVal = Int32.Parse(cardName.Substring(cardName.Length - 2, 2)); //parse the numeric value       
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
