using UnityEngine;

public class EnemyHealth : MonoBehaviour, ICapturable
{
    [SerializeField] Roll.rollType rollType;

    public void GetRolled()
    {
        PanManager.instance.AcquireRoll(rollType);
        Destroy(gameObject);
    }
}
