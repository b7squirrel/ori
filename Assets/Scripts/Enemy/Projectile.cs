using UnityEngine;

/// <summary>
/// 캡쳐 가능한 Projectile 기능 구현
/// </summary>
public class Projectile : MonoBehaviour, ICapturable
{
    [SerializeField] Flavour.flavourType flavourType;
    public void GetCaptured()
    {
        PanManager.instance.AcquireFlavour(flavourType);
    }
}
