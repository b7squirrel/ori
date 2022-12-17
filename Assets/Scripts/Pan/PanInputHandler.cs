using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanInputHandler : MonoBehaviour
{
    public bool CaptureInput { get; private set; }

    [SerializeField] float inputHoldTime;
    float captureInputStartTime;

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
}
