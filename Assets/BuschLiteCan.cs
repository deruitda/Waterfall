using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuschLiteCan : MonoBehaviour
{
    private int clicks = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void placeCard()
    {
        clicks++;
        Debug.Log($"Clicks: {clicks}");
    }
}
