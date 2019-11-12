using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ExplosionEffect : NetworkBehaviour
{
    public GameObject _explisionPrefab;
    public AudioSource _explosionAudio;

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
        _explosionAudio.volume = 1.0f; //for some reason sound only plays when the "Play on Awake" box is ticked, so I default the sound to 0 so it doesn't play on awake, and then set it back to 100
        _explosionAudio.enabled = true;
        _explosionAudio.Play();
        Instantiate(_explisionPrefab, this.transform.position, this.transform.rotation);
        _explosionAudio.Stop();
    }
}
