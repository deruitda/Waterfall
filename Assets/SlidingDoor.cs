using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SlidingDoor : NetworkBehaviour
{
    // Start is called before the first frame update
    private Vector3 startingPos;
    private Vector3 openPos;
    public bool IsOpen;
    void Start()
    {
        startingPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        openPos = new Vector3(startingPos.x - 5, startingPos.y, startingPos.z);
    }

    [Command]
    internal void CmdToggleDoor()
    {
        if (!this.IsOpen)
        {
            //open the door
            this.transform.position = openPos;
            IsOpen = true;
            RpcOpenDoor();
        }
        else
        {
            //close the door
            this.transform.position = startingPos;
            IsOpen = false;
            RpcCloseDoor();
        }
    }

    [ClientRpc]
    private void RpcCloseDoor()
    {
        this.transform.position = startingPos;
        IsOpen = false;
    }

    [ClientRpc]
    private void RpcOpenDoor()
    {
        this.transform.position = openPos;
        IsOpen = true;
    }
}
