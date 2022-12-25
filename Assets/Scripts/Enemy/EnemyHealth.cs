using UnityEngine;

public class EnemyHealth : MonoBehaviour, ICapturable, IDamageable
{
    [SerializeField] Roll.rollType rollType;

    public void GetCaptured()
    {
        PanManager.instance.AcquireRoll(rollType);
        Destroy(gameObject);
    }
    public void TakeDamage()
    {
        Debug.Log("Take Damage");
    }
}
