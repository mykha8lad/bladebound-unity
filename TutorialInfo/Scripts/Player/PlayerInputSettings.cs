using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInputSettings", menuName = "Game/Player Input Settings")]
public class PlayerInputSettings : ScriptableObject
{
    public KeyCode moveForward = KeyCode.W;
    public KeyCode moveBackward = KeyCode.S;
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;
    public KeyCode jump = KeyCode.Space;
    public KeyCode run = KeyCode.LeftShift;

    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float jumpForce = 5f;
    public float lookSensitivity = 2f;
}
