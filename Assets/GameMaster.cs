using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameMaster : NetworkBehaviour
{
    public List<Material> availableMats;
    public PlayingCard _currentSelectedCard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectCard(PlayingCard card)
    {
        _currentSelectedCard = card;
        int rand = Random.Range(0, availableMats.Count - 1);

        card.RpcSetMaterial(rand);
        RpcRemoveMatFromAvailable(rand);
    }

    [ClientRpc]
    public void RpcRemoveMatFromAvailable(int index)
    {
        availableMats.RemoveAt(index);
    }

    public Material GetMat(int index)
    {
        Material mat = availableMats[index];
        return mat;
    }
}
