﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public List<Material> availableMats;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Material GetMat()
    {
        int index = Random.Range(0, availableMats.Count - 1);
        Material mat = availableMats[index];
        availableMats.Remove(mat);
        return availableMats[index];
    }
}
