using System.Collections;
using UnityEngine;

/// <summary>
/// ���� �浹�ϸ� EnemyHealth�� TakeDamage() ȣ��, Life ����
/// </summary>
public class RollGen : MonoBehaviour
{
    [SerializeField] protected float life;

    [field : SerializeField]
    public Roll.rollType RollType { get; set; }

    protected bool isFlying;

    #region Unity CallBack Functions
    void Update()
    {
        if (isFlying)
            CheckLife();
    }
    #endregion

    #region Check Functions
    void CheckLife()
    {
        StartCoroutine(CheckLifeCo());
    }
    IEnumerator CheckLifeCo()
    {
        yield return new WaitForSeconds(life);
        Destroy(gameObject);
    }
    #endregion

    #region Set Functions
    public void SetRollToFlying()
    {
        isFlying = true;
    }
    #endregion

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<IDamageable>().TakeDamage();
        }
    }
}
