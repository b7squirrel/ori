using System.Collections;
using UnityEngine;

/// <summary>
/// 적과 충돌하면 EnemyHealth의 TakeDamage() 호출
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
        WhiteFlash(.1f); // 약간 딜레이를 줘야 읽히더라
        WhiteFlash(.48f); // 팬 위에 랜딩
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
