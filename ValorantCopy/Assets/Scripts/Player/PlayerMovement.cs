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
        //Yön tuþlarýyla oyuncuyu hareket ettirir.
        Vector3 moveInputs = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        //Shift tuþuna basýlýysa hýzlý koþarsa basýlý deðilse normal hýzda koþar.
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
        //Yön tuþlarýndan herhangi biri basýlýysa koþma animasyonu çalýþýr.
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
        //Y ekseninde Player objesini döndürür
        transform.Rotate(0f, Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity, 0f);
        //X ekseninde kamerayý belirli bir aralýkta döndürür.
        cameraX -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        cameraX = Mathf.Clamp(cameraX, minCameraX, maxCameraX);
        Camera.main.transform.localRotation = Quaternion.Euler(cameraX, 0f, 0f);
    }
}
