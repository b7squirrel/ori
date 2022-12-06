using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; }
    public bool JumpInput { get; private set; }

    void Update()
    {
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
        }
    }
    public void UseJumpInput() => JumpInput = false;
}
