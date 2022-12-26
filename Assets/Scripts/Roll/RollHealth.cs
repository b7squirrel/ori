using System.Collections;
using UnityEngine;

/// <summary>
/// 롤 LifeTime 관리, 캡쳐 될 경우 rollType과 flavourType을 panManager에 전달 
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
