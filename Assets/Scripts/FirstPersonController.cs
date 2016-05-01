using UnityEngine;
using System.Collections;

public class FirstPersonController : MonoBehaviour {

    public float movementSpeed = 5.0f;
    public float mouseSensitivity = 3.0f;
    public float jumpSpeed = 5.0f;

    private float verticalRotation = 0;
    public float upDownRange = 60.0f;

    private float verticalVelocity = 0.0f;

    public Camera playerCamera;
    private CharacterController characterController;

	void Start () {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        characterController = GetComponent<CharacterController>();
    }
	
	void Update () {

        float leftRightRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, leftRightRotation, 0);

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
        float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;

        verticalVelocity += Physics.gravity.y * Time.deltaTime * 1.5f;

        if (characterController.isGrounded && Input.GetButtonDown("Jump"))
            verticalVelocity = jumpSpeed;

        Vector3 speed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed);
        speed = transform.rotation * speed;

        characterController.Move(speed * Time.deltaTime);
	}
}
