using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : NetworkBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    private PlayerMotor _motor;

    [SerializeField]
    private float _lookSens = 5f;

    public float _gazeDistance = 5f;
    public PlayingCard _currSelectedCard;

    // Start is called before the first frame update
    void Start()
    {
        _motor = GetComponent<PlayerMotor>();
        _currSelectedCard = GameObject.FindGameObjectWithTag("PlayingCard").GetComponent<PlayingCard>();//TODO: fix this to work with multiple cards cardGO.GetComponent<PlayingCard>();
    }

    // Update is called once per frame
    void Update()
    {
        //calculate our movement velocity as a 3D vec
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 movHoriz = transform.right * xMov;
        Vector3 movVert = transform.forward * zMov;

        //final movement vector
        Vector3 velocity = (movHoriz + movVert).normalized * _speed;

        //apply transoform
        _motor.SetVelocity(velocity);

        //get horiz mouse rotation (looking around)
        float yRot = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0f, yRot, 0f) * _lookSens;

        _motor.SetRotation(rotation);

        //get vert mouse rotation (camera roatation up and down)
        float xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 cameraRotation = new Vector3(xRot, 0f, 0f) * _lookSens;

        _motor.SetCameraRotation(cameraRotation);

        //if the player presses E, and hits a playing card with their gaze
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward, Color.red);
        if (Input.GetKeyUp(KeyCode.E)
            && Physics.Raycast(this.transform.position, transform.forward, out hit, 10f)
            && hit.collider.gameObject.CompareTag("PlayingCard")
            && hit.distance < _gazeDistance)
        {
            GameObject cardGO = hit.collider.gameObject;

            CmdSelectCard();
        }
    }

    [Command]
    private void CmdSelectCard()
    {
        _currSelectedCard.Select();
    }
}
