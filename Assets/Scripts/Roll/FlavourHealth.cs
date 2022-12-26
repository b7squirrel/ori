using UnityEngine;

/// <summary>
/// 캡쳐 가능한 Flavour 기능 구현
/// </summary>
public class FlavourHealth : MonoBehaviour, ICapturable
{
    public void GetCaptured()
    {
        PanManager.instance.AcquireFlavour(GetComponent<FlavourGen>().FlavourType);
        Destroy(gameObject);
    }
}
