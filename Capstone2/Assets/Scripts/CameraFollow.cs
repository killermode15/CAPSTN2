using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    protected Transform _XForm_Camera;
    protected Transform _XForm_Parent;

    protected Vector3 _LocalRotation;
    protected float _CameraDistance = 10f;

    public float MouseSensitivity = 4f;
    public float OrbitDampening = 10f;


    // Use this for initialization
    void Start()
    {
        this._XForm_Camera = this.transform;
        this._XForm_Parent = this.transform.parent;

    }


    void Update()
    {
        //Rotation of the Camera based on Mouse Coordinates
        //if (Input.GetAxis("RightStickX") != 0 || Input.GetAxis("RightStickY") != 0)
        //{
            _LocalRotation.x += Input.GetAxis("RightStickX") * MouseSensitivity;
            _LocalRotation.x += Input.GetAxis("RightStickY") * MouseSensitivity;

            //Clamp the y Rotation to horizon and not flipping over at the top
            if (_LocalRotation.y < 0f)
                _LocalRotation.y = 0f;
            else if (_LocalRotation.y > 90f)
                _LocalRotation.y = 90f;
        //}

        /*//Actual Camera Rig Transformations
        Quaternion QT = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
        this._XForm_Parent.rotation = Quaternion.Lerp(this._XForm_Parent.rotation, QT, Time.deltaTime * OrbitDampening);*/
    }
}
