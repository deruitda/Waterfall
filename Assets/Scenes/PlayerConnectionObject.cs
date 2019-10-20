using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerConnectionObject : NetworkBehaviour
{
    public GameObject PlayerUnitPrefab;
    [SyncVar] //if a sync var is changed on the SERVER, then all clients will be updated with the new value
    public string PlayerName;
    public Camera PlayerCam;
    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer)
        {
            CmdSpawnUnit();
            //PlayerCam = PlayerUnitPrefab.
            //PlayerCam.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            PlayerName = "TEST" + Random.Range(0, 100).ToString();
            Debug.Log("Send server new name");
            CmdChangePlayerName(PlayerName);
        }
    }

    [Command]
    void CmdSpawnUnit()
    {
        GameObject gameObject = Instantiate(PlayerUnitPrefab);

        NetworkServer.SpawnWithClientAuthority(gameObject, connectionToClient);
    }

    [Command]
    void CmdChangePlayerName(string name)
    {
        Debug.Log($"Player name: {name}");
        PlayerName = name;
    }
}
