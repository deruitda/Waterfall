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

    public void Select(int playerTurn)
    {
        _gm.SelectCard(this, playerTurn);
    }

    [ClientRpc]
    public void RpcSetMaterial(int rand)
    {
        _meshRenderer.material = _gm.GetMat(rand);
    }
}
