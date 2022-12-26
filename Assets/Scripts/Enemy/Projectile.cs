using UnityEngine;

/// <summary>
/// ĸ�� ������ Projectile ��� ����
/// </summary>
public class Projectile : MonoBehaviour, ICapturable
{
    [SerializeField] Flavour.flavourType flavourType;
    public void GetCaptured()
    {
        PanManager.instance.AcquireFlavour(flavourType);
    }
}
