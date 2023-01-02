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
    /// ���� ���� ���Կ� ��ġ�ϰ�, ���� ���Կ��� �����ϰ�(IsEmpty), ���� ���� ���Կ� Add�ϰ�, Sorting Order--
    /// </summary>
    public void MoveRoll(PanSlot targetSlot)
    {
        GameObject roll = GetRoll();
        roll.transform.SetPositionAndRotation(targetSlot.transform.position, targetSlot.transform.rotation);
        RemoveRoll();
        targetSlot.AddRoll(roll.transform);
    }
    /// <summary>
    /// IPhysics ������, ���ư� ���� ����, layerMask�� Roll�� ����(���� �����Ǹ� ĸ���� �� �νĵ��� �ʵ��� "Captured"�� ������)
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
