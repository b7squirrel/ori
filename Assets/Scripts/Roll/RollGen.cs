using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollGen : MonoBehaviour
{
    [SerializeField] protected Roll.rollType rollType;
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyHealth>().TakeDamage();
        }
    }
}
