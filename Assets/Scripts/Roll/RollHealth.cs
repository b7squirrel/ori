using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollHealth : MonoBehaviour, ICapturable
{
    public void GetCaptured()
    {
        PanManager.instance.AcquireRoll(GetComponent<RollGen>().RollType);
        Destroy(gameObject);
    }
}
