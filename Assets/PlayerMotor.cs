using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{

    private Vector3 _velocity = Vector3.zero;
    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetVelocity(Vector3 velocity)
    {
        _velocity = velocity;
    }

    // run every physics frame
    void FixedUpdate()
    {
        PerformMovement();
    }

    private void PerformMovement()
    {
        if(_velocity != Vector3.zero)
        {
            //this is better than adding force or updating the transform because this will perform physics checks for us
            _rigidbody.MovePosition(_rigidbody.position + _velocity * Time.fixedDeltaTime);
        }
    }
}
