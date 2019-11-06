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

    // Start is called before the first frame update
    void Start()
    {
        playingMats = availableMats; //< TODO: Need to clone, not set equal to
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        Debug.Log("New game started!");
        playingMats = availableMats;
        _can.NewCan();
        Debug.Log("Cards: " + playingMats.Count);
    }

    public void SelectCard(PlayingCard card)
    {
        _currentSelectedCard = card;
        int rand = Random.Range(0, playingMats.Count - 1);

        card.RpcSetMaterial(rand);
        RpcRemoveMatFromAvailable(rand);
    }

    [ClientRpc]
    public void RpcRemoveMatFromAvailable(int index)
    {
        playingMats.RemoveAt(index);
        _can.placeCard();
        Debug.Log(playingMats.Count);

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
}
