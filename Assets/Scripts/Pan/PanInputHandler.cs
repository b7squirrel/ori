using UnityEngine;
using UnityEngine.InputSystem;

public class PanInputHandler : MonoBehaviour
{
    public bool CaptureInput { get; private set; }
    public bool HitRollInput { get; private set; }

    [SerializeField] float inputHoldTime;
    float captureInputStartTime;
    float hitRollInputStartTime;

    void Update()
    {
        CheckCaptureInputHoldTime();
        CheckHitRollInputHoldTime();
    }
    
    public void OnCaptureInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            CaptureInput = true;
            captureInputStartTime = Time.time;
        }
    }
    public void OnHitRollInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            HitRollInput = true;
            hitRollInputStartTime = Time.time;
        }
    }
    public void CheckCaptureInputHoldTime()
    {
        if (Time.time > captureInputStartTime + inputHoldTime)
        {
            CaptureInput = false;
        }
    }
    public void CheckHitRollInputHoldTime()
    {
        if (Time.time > hitRollInputStartTime + inputHoldTime)
        {
            HitRollInput = false;
        }
    }
}
