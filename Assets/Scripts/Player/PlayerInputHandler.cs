using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool DashInput { get; private set; }

    [SerializeField] float inputHoldTime;

    float jumpInputStartTime;
    float dashInputStartTime;

    void Update()
    {
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
        GetMovementInput();
        Jump();
        Dash();
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
    public void Dash()
    {
        if (Input.GetKeyDown(KeyCode.S) && MovementInput.x != 0)
        {
        DashInput = true;
        dashInputStartTime = Time.time;
        }
    }
    public void UseJumpInput() => JumpInput = false;
    public void UseDashInput() => DashInput = false;
    void CheckJumpInputHoldTime()
    {
        if (Time.time > jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }
    void CheckDashInputHoldTime()
    {
        if (Time.time > dashInputStartTime + inputHoldTime)
        {
            DashInput = false;
        }
    }
}
