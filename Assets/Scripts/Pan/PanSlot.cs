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
        return transform.GetComponentInChildren<RollGen>().gameObject;
    }

    public void DumpRoll()
    {
        GetRoll().GetComponent<IPhysics>().AddDumpRollPhysics();
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
    public void MoveRoll(PanSlot targetSlot)
    {
        GameObject roll = GetRoll();
        roll.transform.SetPositionAndRotation(targetSlot.transform.position, targetSlot.transform.rotation);
        RemoveRoll();
        targetSlot.AddRoll(roll.transform);
    }
    public void ReleaseRoll()
    {
        Pan pan = GetComponentInParent<Pan>();
        float xDir = pan.GetComponentInParent<PlayerMouseDirection>().GetMouseHorizontalDirection();
        GetRoll().GetComponent<IPhysics>().AddHitRollPhysics(xDir);
        RemoveRoll();
    }
}
