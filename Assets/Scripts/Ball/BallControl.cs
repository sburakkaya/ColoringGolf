using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private float xRot, yRot = 0f;
    private ColorManager _colorManager;
    [SerializeField] private Rigidbody ball;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float shootPower;
    [SerializeField] private LineRenderer line;
    private GameObject _flag;
    private CameraControl cameraControl;
    private GameObject cameraHolder;
    private Vector3 lastPosition;
    private Vector3 respawnPoint = new Vector3(0,1.5f,0);
    private Quaternion lastQuaternion;
    public bool dragging;
    public bool moving;
    public bool flagArea;
    public bool isLooked;
    private float lineRendererLength = 0.5f;

    public float Get_XRot()
    {
        return xRot;
    }
    
    void Start()
    {
        cameraControl = GameObject.Find("CameraControl").GetComponent<CameraControl>();
        cameraHolder = GameObject.Find("CameraHolder");
        _colorManager = FindObjectOfType<ColorManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CorrectHole"))
        {
            Debug.Log("true hole");
            collision.gameObject.tag = "Untagged";
            _colorManager.Colorize();
        }
        if (collision.gameObject.CompareTag("WrongHole"))
        {
            transform.position = respawnPoint;
            xRot = 0;
            transform.rotation = lastQuaternion;
            ball.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Flag"))
        {
            /*Vector3 newPosition = cameraHolder.transform.position;
            Vector3 newRotation = cameraHolder.transform.eulerAngles;
            newPosition.y = 6;
            newRotation.x = 31;
            cameraHolder.transform.position = newPosition;
            cameraHolder.transform.eulerAngles = newRotation;*/

            //cameraHolder.transform.DOMoveY(5, 0.5f);
            //cameraHolder.transform.DOMoveZ(-3.5f, 0.5f);
            _flag = other.gameObject;
            flagArea = true;
            isLooked = false;
        }
    }

    void OnFlagArea()
    {
        if (moving == false && flagArea == true && dragging == false && isLooked == false)
        {
            lineRendererLength = 0.2f;
            shootPower = 1;
            cameraHolder.transform.DOLocalMove(new Vector3(0,6.5f,-4), 0.5f);
            Vector3 newRotation = cameraHolder.transform.eulerAngles;
            newRotation.x = 45;
            newRotation.y = 0;
            newRotation.z = 0;
            //cameraHolder.transform.eulerAngles = newRotation;
            cameraHolder.transform.DOLocalRotate(newRotation, 0.5f);
            transform.DOLookAt(_flag.transform.position, 0.5f);
            //xRot = Mathf.Lerp(xRot,-transform.rotation.eulerAngles.y,0.1f);
            DOTween.To(() => xRot,  x=> xRot = x, -transform.rotation.eulerAngles.y, 0.1f).SetEase(Ease.InOutQuad);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Flag"))
        {
            lineRendererLength = 0.5f;
            shootPower = 2;
            Vector3 newPosition = cameraHolder.transform.position;
            Vector3 newRotation = cameraHolder.transform.eulerAngles;
            newPosition.x = 0;
            newPosition.y = 3.87f;
            newPosition.z = -8.45f;
            newRotation.x = 10;
            cameraHolder.transform.localPosition = newPosition;
            cameraHolder.transform.eulerAngles = newRotation;
            flagArea = false;
        }
    }

    private void OnMouseDrag()
    {
        dragging = true;
    }

    void FixedUpdate()
    {
        if (ball.velocity.magnitude > 0.3)
        {
            moving = true;
        }
        else
        {
            moving = false;
            OnFlagArea();
        }
        
        if (transform.position.y < -20)
        {
            ball.velocity = Vector3.zero;
            transform.position = lastPosition;
        }
    }

    private void Update()
    {
        DragForce();
        LookAround();
    }

    void LookAround()
    {
        if (Input.GetMouseButton(0) && dragging == false)
        {
            xRot += Input.GetAxis("Mouse X") * rotationSpeed;
            transform.rotation = Quaternion.Euler(0,-xRot,0);
            isLooked = true;
        }
    }

    private void DragForce()
    {
        if (Input.GetMouseButton(0) && dragging && moving == false)
        {
            lastPosition = transform.position;
            lastQuaternion = transform.rotation;
            xRot += Input.GetAxis("Mouse X")*rotationSpeed;
            yRot += Input.GetAxis("Mouse Y")*rotationSpeed;
            if (yRot < -30f)
            {
                yRot = -30f;
            }
            if (yRot > 0)
            {
                yRot = 0;
            }
            transform.rotation = Quaternion.Euler(0,-xRot,0f);
            line.gameObject.SetActive(true);
            line.SetPosition(0,transform.position);
            line.SetPosition(1,transform.position + (transform.forward * lineRendererLength)*-yRot);
        }

        if (Input.GetMouseButtonUp(0) && dragging && moving == false)
        {
            ball.velocity = transform.forward * -yRot *shootPower;
            line.gameObject.SetActive(false);
            dragging = false;
            yRot = 0;
        }
    }
}
