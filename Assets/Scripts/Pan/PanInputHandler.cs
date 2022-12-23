using UnityEngine;

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
        Capture();
    }

    public void Capture()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CaptureInput = true;
            captureInputStartTime = Time.time;
        }
    }
    public void CheckCaptureInputHoldTime()
    {
        if (Time.time > captureInputStartTime + inputHoldTime)
        {
            CaptureInput = false;
        }
    }

    public void HitRoll()
    {
        if (Input.GetMouseButton(1))
        {
            HitRollInput = true;
            hitRollInputStartTime = Time.time;
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
