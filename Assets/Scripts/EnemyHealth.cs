using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, ICapturable
{
    [SerializeField] GameObject roll;
    [SerializeField] Transform captureSlot;

    public void GetRolled()
    {
        GameObject _roll = Instantiate(roll, captureSlot.position, captureSlot.rotation);
        _roll.transform.SetParent(captureSlot);
        Destroy(gameObject);
    }
}
