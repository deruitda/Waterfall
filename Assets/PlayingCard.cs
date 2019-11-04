using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingCard : MonoBehaviour
{
    public Material _mat;

    public void Select()
    {
        if(_mat == null)
        {
            Debug.Log("Card material is empty");
            return;
        }

        _mat.color = Color.blue;
    }
}
