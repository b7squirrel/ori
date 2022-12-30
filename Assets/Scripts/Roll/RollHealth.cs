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

    bool isCaptured; // �ߺ����� ĸ�ĵ��� �ʵ���

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
