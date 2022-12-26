using UnityEngine;

/// <summary>
/// ĸ�� ������ Flavour ��� ����
/// </summary>
public class FlavourHealth : MonoBehaviour, ICapturable
{
    public void GetCaptured()
    {
        PanManager.instance.AcquireFlavour(GetComponent<FlavourGen>().FlavourType);
        Destroy(gameObject);
    }
}
