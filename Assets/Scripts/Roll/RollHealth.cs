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

    bool isCaptured; // 중복으로 캡쳐되지 않도록

    public void CountLife()
    {
        StartCoroutine(CountLifeCo());
    }

    IEnumerator CountLifeCo()
    {
        yield return new WaitForSeconds(lifeTime);
        GetComponent<IRollAction>().RollAction();
        Destroy(gameObject);
    }

    public void GetCaptured()
    {
        if (isCaptured)
            return;

        PanManager.instance.AcquireRoll(RollType);
        if (FlavourType != Flavour.flavourType.none)
        {
            PanManager.instance.AcquireFlavour(FlavourType);
        }
        isCaptured = true;
        Destroy(gameObject);
    }
}
