using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingCard : MonoBehaviour
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

        _meshRenderer.material = _gm.GetMat();
    }
}
