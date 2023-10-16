using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float MouseSensitivity;
    public Transform CamTransform;
    private float camRotation = 0f;

    public float MoveSpeed;
    public float Gravity = -9.8f;
    public float JumpSpeed;

    public float verticalSpeed;

    public CharacterController CC;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector3 movement = Vector3.zero;

        float forwardMovement = Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;
        float sideMovement = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;

        movement += (transform.forward * forwardMovement) + (transform.right * sideMovement);

        if (CC.isGrounded)
        {
            verticalSpeed = 0f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalSpeed = JumpSpeed;
            }
        }

        verticalSpeed += (Gravity * Time.deltaTime);
        movement += (transform.up * verticalSpeed * Time.deltaTime);

        CC.Move(movement);

        float mouseInputY = Input.GetAxis("Mouse Y") * MouseSensitivity;
        camRotation -= mouseInputY;
        camRotation = Mathf.Clamp(camRotation, -90f, 90f);
        CamTransform.localRotation = Quaternion.Euler(new Vector3(camRotation, 0f, 0f));

        float mouseInputX = Input.GetAxis("Mouse X") * MouseSensitivity;
        transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0f, mouseInputX, 0f));
    }
}
