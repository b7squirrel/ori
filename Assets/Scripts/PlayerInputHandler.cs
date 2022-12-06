using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; }
    public bool JumpInput { get; private set; }

    [SerializeField] float inputHoldTime;
    float jumpInputStartTime;

    void Update()
    {
        CheckJumpInputHoldTime();
        GetMovementInput();
        Jump();
    }
    void GetMovementInput()
    {
        MovementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            JumpInput = true;
            jumpInputStartTime = Time.time;
        }
    }
    public void UseJumpInput() => JumpInput = false;
    void CheckJumpInputHoldTime()
    {
        if (Time.time > jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }
}
