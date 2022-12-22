using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, ICapturable
{
    [SerializeField] GameObject roll;

    public void GetRolled(Transform captureSlot)
    {
        GameObject _roll = Instantiate(roll, captureSlot.position, captureSlot.rotation);
        _roll.transform.SetParent(captureSlot);
        Destroy(gameObject);
    }
}
