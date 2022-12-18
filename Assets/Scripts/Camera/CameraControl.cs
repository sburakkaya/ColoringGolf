using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Rigidbody target;
    private BallControl _ballControl;
    public float rotationSpeed;
    void Awake()
    {
        _ballControl = target.GetComponent<BallControl>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = target.position;
        //transform.rotation = target.rotation;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, -_ballControl.Get_XRot(), transform.eulerAngles.z);
    }
}
