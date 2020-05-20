using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlackBoardTextEditor : MonoBehaviour
{
    public TextMeshPro BlackBoardText;

    void Start()
    {
        if (BlackBoardText == null)
            BlackBoardText = this.gameObject.GetComponent<TextMeshPro>();
    }

    public void SetBoardText(string text)
    {
        BlackBoardText.text = text;
    }
}
