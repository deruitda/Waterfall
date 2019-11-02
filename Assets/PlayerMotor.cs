using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera _cam;

    private Vector3 _velocity = Vector3.zero;
    private Vector3 _rotation = Vector3.zero;
    private Rigidbody _rigidbody;
    private Vector3 _cameraRot;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetVelocity(Vector3 velocity)
    {
        _velocity = velocity;
    }

    public void SetRotation(Vector3 rotation)
    {
        _rotation = rotation;
    }

    public void SetCameraRotation(Vector3 cameraRot)
    {
        _cameraRot = cameraRot;
    }

    // run every physics frame
    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    private void PerformMovement()
    {
        if(_velocity != Vector3.zero)
        {
            //this is better than adding force or updating the transform because this will perform physics checks for us
            _rigidbody.MovePosition(_rigidbody.position + _velocity * Time.fixedDeltaTime);
        }
    }

    private void PerformRotation()
    {
        //rotate right to left
        if(_rotation != Vector3.zero)
        {
            _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(_rotation));
        }

        //rotate camera up and down
        if (_cam != null && _cameraRot != Vector3.zero)
        {
            _cam.transform.Rotate(-_cameraRot);
        }
    }
}
