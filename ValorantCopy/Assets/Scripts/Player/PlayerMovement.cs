using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController cc;
    private Rigidbody rb;
    public Animator anim;
    //Move Settings
    [SerializeField] private float speed;
    [SerializeField] private float runSpeed;
    private Vector3 moveVelocity;

    //Camera Control Settings
    [SerializeField] private float mouseSensitivity = 100f;
    private float cameraX;
    private float maxCameraX = 40f;
    private float minCameraX = -80f;

    //Crosshairs
    public GameObject crosshair;
    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        Move();
        CameraController();
    }
    void Move()
    {
        //Y�n tu�lar�yla oyuncuyu hareket ettirir.
        Vector3 moveInputs = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        //Shift tu�una bas�l�ysa h�zl� ko�arsa bas�l� de�ilse normal h�zda ko�ar.
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveVelocity = moveInputs * Time.deltaTime * runSpeed;
            anim.speed = 1.5f;
        }
        else
        {
            moveVelocity = moveInputs * Time.deltaTime * speed;
            anim.speed = 1;
        }
        //Y�n tu�lar�ndan herhangi biri bas�l�ysa ko�ma animasyonu �al���r.
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            anim.SetBool("Running", true);
            crosshair.GetComponent<Animator>().SetBool("isBig", true);
        }
        else
        {
            anim.SetBool("Running", false);
            crosshair.GetComponent<Animator>().SetBool("isBig", false);
        }
        cc.Move(moveVelocity);
    }
    void CameraController()
    {
        //Y ekseninde Player objesini d�nd�r�r
        transform.Rotate(0f, Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity, 0f);
        //X ekseninde kameray� belirli bir aral�kta d�nd�r�r.
        cameraX -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        cameraX = Mathf.Clamp(cameraX, minCameraX, maxCameraX);
        Camera.main.transform.localRotation = Quaternion.Euler(cameraX, 0f, 0f);
    }
}
