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
    private float _cameraRotX = 0f;
    private float _currentCameraRotX = 0f;

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

    public void SetCameraRotation(float cameraRotX)
    {
        _cameraRotX = cameraRotX;
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
        if (_cam != null && _cameraRotX != 0f)
        {
            // Set our rotation and clamp it
            _currentCameraRotX -= _cameraRotX;
            
            _currentCameraRotX = Mathf.Clamp(_currentCameraRotX, -45, 45);

            //Apply our rotation to the transform of our camera
            _cam.transform.localEulerAngles = new Vector3(_currentCameraRotX, 0f, 0f);
        }
    }
}
