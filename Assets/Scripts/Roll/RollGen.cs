using UnityEngine;

/// <summary>
/// 적과 충돌하면 EnemyHealth의 TakeDamage() 호출
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
