using UnityEngine;

public class PanSlot : MonoBehaviour
{
    public bool IsEmpty { get; private set; } = true;
    
    public void AddRoll(Transform roll)
    {
        roll.SetPositionAndRotation(transform.position, transform.rotation);
        roll.SetParent(transform);
        IsEmpty = false;
    }

    public GameObject GetRoll()
    {
        return transform.GetChild(0).gameObject;
    }

    public void DumpRoll()
    {
        Rigidbody2D rb = GetRoll().AddComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
        rb.gravityScale = 5f;
        BoxCollider2D boxCol = GetRoll().AddComponent<BoxCollider2D>();
        boxCol.size = new Vector2(1f, .8f);

        RemoveRoll();
    }

    public void RemoveRoll()
    {
        GetRoll().transform.SetParent(null);
        IsEmpty = true;
    }

    /// <summary>
    /// 롤을 다음 슬롯에 배치하고, 롤을 슬롯에서 제거하고(IsEmpty), 롤을 다음 슬롯에 Add하고, Sorting Order--
    /// </summary>
    public void PushRoll(PanSlot targetSlot)
    {
        GameObject roll = GetRoll();
        roll.transform.SetPositionAndRotation(targetSlot.transform.position, targetSlot.transform.rotation);
        RemoveRoll();
        targetSlot.AddRoll(roll.transform);
        roll.GetComponent<SpriteRenderer>().sortingOrder--;
    }
}
