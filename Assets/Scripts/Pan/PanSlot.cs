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
        Destroy(transform.GetChild(0).gameObject);
    }

    public void RemoveRoll()
    {
        GetRoll().transform.SetParent(null);
        IsEmpty = true;
    }

    public void PushRoll(PanSlot targetSlot)
    {
        GameObject roll = GetRoll();
        roll.transform.SetPositionAndRotation(targetSlot.transform.position, targetSlot.transform.rotation);
        RemoveRoll();
        targetSlot.AddRoll(roll.transform);
        roll.GetComponent<SpriteRenderer>().sortingOrder--;
    }
}
