using UnityEngine;
using Zenject;

public class PlayerMovementFPS : MonoBehaviour
{
    [Inject]
    public PlayerInputSettings inputSettings;

    private CharacterController characterController;
    private Vector3 velocity;
    private Camera playerCamera;

    private float verticalLookRotation = 0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Move();
        ApplyGravity();
        Jump();
        LookAround();
    }

    void Move()
    {
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(inputSettings.moveForward)) moveZ += 1;
        if (Input.GetKey(inputSettings.moveBackward)) moveZ -= 1;
        if (Input.GetKey(inputSettings.moveRight)) moveX += 1;
        if (Input.GetKey(inputSettings.moveLeft)) moveX -= 1;

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        float speed = Input.GetKey(inputSettings.run) ? inputSettings.runSpeed : inputSettings.walkSpeed;

        characterController.Move(move * speed * Time.deltaTime);
    }

    void ApplyGravity()
    {
        if (characterController.isGrounded)
        {
            velocity.y = 0f;
        }
        else
        {
            velocity.y += Physics.gravity.y * Time.deltaTime;
        }

        characterController.Move(velocity * Time.deltaTime);
    }

    void Jump()
    {
        if (characterController.isGrounded && Input.GetKeyDown(inputSettings.jump))
        {
            velocity.y = Mathf.Sqrt(inputSettings.jumpForce * -2f * Physics.gravity.y);
        }
    }

    void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * inputSettings.lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * inputSettings.lookSensitivity;
        
        verticalLookRotation -= mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(verticalLookRotation, 0f, 0f);
        
        transform.Rotate(Vector3.up * mouseX);
    }
}
