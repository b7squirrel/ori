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
    /// <summary>
    /// IPhysics 입히고, 날아갈 방향 설정, layerMask를 Roll로 설정(롤은 생성되면 캡쳐할 때 인식되지 않도록 "Captured"로 설정됨)
    /// </summary>
    public void ReleaseRoll(float movementInputDir, float playerVelocity)
    {
        Pan pan = GetComponentInParent<Pan>();
        float mouseDir = pan.GetComponentInParent<PlayerMouseDirection>().GetMouseHorizontalDirection();
        float playerFacingDir = pan.GetComponentInParent<Player>().FacingDirection;
        GetRoll().GetComponent<IPhysics>().AddHitRollPhysics(mouseDir, movementInputDir, playerFacingDir, playerVelocity);
        GetRoll().layer = LayerMask.NameToLayer("Roll"); 
        RemoveRoll();
    }
}
