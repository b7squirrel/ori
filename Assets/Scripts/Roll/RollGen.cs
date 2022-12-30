using System.Collections;
using UnityEngine;

/// <summary>
/// ���� �浹�ϸ� EnemyHealth�� TakeDamage() ȣ��
/// </summary>
public class RollGen : MonoBehaviour, IRollEffect
{
    protected SpriteRenderer mSR;
    protected Material initialMat;

    private void Awake()
    {
        mSR = GetComponent<SpriteRenderer>();
        initialMat = mSR.material;
    }
    void OnEnable()
    {
        WhiteFlash(.1f); // �ణ �����̸� ��� ��������
        WhiteFlash(.48f); // �� ���� ����
    }

    public void WhiteFlash(float delayTime)
    {
        StartCoroutine(WhiteFlashCo(delayTime));
    }
    
    IEnumerator WhiteFlashCo(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        mSR.material = GameManager.instance.WhiteMat;
        yield return new WaitForSeconds(GameManager.instance.duration);
        mSR.material = initialMat;
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<IDamageable>().TakeDamage();
        }
    }
}
