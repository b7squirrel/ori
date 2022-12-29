using UnityEngine;

[CreateAssetMenu(fileName = "newPanData", menuName = "Data/Pan/Base Data")]
public class PanData : ScriptableObject
{
    [Header("Capture")]
    public float panCaptureTime;
    public float panCaptureCoolTime;
    public LayerMask whatIsCapturable;

    [Header("Hit Roll")]
    public GameObject hitRollSpark;

    [Header("Flavour")]
    public float flavourLife;
}
