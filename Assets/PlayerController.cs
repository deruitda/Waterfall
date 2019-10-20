using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    private PlayerMotor _motor;

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
    }
}
