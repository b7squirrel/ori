using UnityEngine;

/// <summary>
/// ���� �浹�ϸ� EnemyHealth�� TakeDamage() ȣ��
/// </summary>
public class RollGen : MonoBehaviour
{
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<IDamageable>().TakeDamage();
        }
    }
}
