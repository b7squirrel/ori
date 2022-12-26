using System.Collections;
using UnityEngine;

/// <summary>
/// �� LifeTime ����, ĸ�� �� ��� rollType�� flavourType�� panManager�� ���� 
/// </summary>
public class RollHealth : MonoBehaviour, ICapturable
{
    [field: SerializeField]
    public Roll.rollType RollType { get; set; }
    public Flavour.flavourType FlavourType { get; set; } = Flavour.flavourType.none;

    [SerializeField] protected float lifeTime;

    public void CountLife()
    {
        StartCoroutine(CountLifeCo());
    }

    public IEnumerator CountLifeCo()
    {
        throw new System.NotImplementedException();
    }

    public void GetCaptured()
    {
        PanManager.instance.AcquireRoll(RollType);
        if (FlavourType != Flavour.flavourType.none)
        {
            PanManager.instance.AcquireFlavour(FlavourType);
        }
        Destroy(gameObject);
    }
    
}
