using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayingCard : NetworkBehaviour
{
    public MeshRenderer _meshRenderer;
    public GameMaster _gm;

    public void Select()
    {
        if(_meshRenderer == null)
        {
            Debug.Log("Card renderer is empty");
            return;
        }

        CmdSetMaterial();
    }

    [Command]
    private void CmdSetMaterial()
    {
        int rand = UnityEngine.Random.Range(0, _gm.availableMats.Count - 1);
        RpcSetMaterial(rand);
    }

    [ClientRpc]
    private void RpcSetMaterial(int rand)
    {
        _meshRenderer.material = _gm.GetMat(rand);
    }
}
