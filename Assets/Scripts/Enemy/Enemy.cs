using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected EnemyHealth enemyHealth;

    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        
    }
}
