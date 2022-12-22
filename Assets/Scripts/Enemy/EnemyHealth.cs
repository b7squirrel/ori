using UnityEngine;

public class EnemyHealth : MonoBehaviour, ICapturable
{
    [field: SerializeField]
    public Roll.rollType RollType { get; private set; }
    RollSo rollso;

    public void GetRolled(Transform captureSlot)
    {
        rollso = RecipeRoll.instance.GetRollSo(RollType);
        GameObject rollPrefab = rollso.rollPrefab;

        GameObject _roll = Instantiate(rollPrefab, captureSlot.position, captureSlot.rotation);
        _roll.transform.SetParent(captureSlot);
        Destroy(gameObject);
    }
}
