using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] _componentsToDisable;

    Camera _mainCam;

    // Start is called before the first frame update
    void Start()
    {
        //if this object isn't controlled by the player, disable components
        if(!isLocalPlayer)
        {
            for (int i = 0; i < _componentsToDisable.Length; i++)
            {
                _componentsToDisable[i].enabled = false;
            }
        }
        //else we just want to disable the main camera when the player joins
        else
        {
            _mainCam = Camera.main;
            if(_mainCam != null)
                _mainCam.gameObject.SetActive(false);
        }
    }

    //enable the scene cam when the player disconnects
    private void OnDisable()
    {
        if (_mainCam != null)
            _mainCam.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
