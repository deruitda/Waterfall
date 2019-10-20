using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    private PlayerMotor _motor;

    [SerializeField]
    private float _lookSens = 5f;

    // Start is called before the first frame update
    void Start()
    {
        _motor = GetComponent<PlayerMotor>();
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
    }
}
