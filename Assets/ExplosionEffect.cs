using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ExplosionEffect : NetworkBehaviour
{
    public GameObject _explisionPrefab;

    public void Explode()
    {
        CmdExplode();
    }

    [Command]
    void CmdExplode()
    {
        RpcExplode();
    }

    [ClientRpc]
    private void RpcExplode()
    {
        Instantiate(_explisionPrefab, this.transform.position, this.transform.rotation);
    }
}
