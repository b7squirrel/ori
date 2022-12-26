using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Flavour Action을 상속받아 다양한 액션을 실행.
/// </summary>
public class FlavourFire : FlavourGen, IFlavourAction
{
    public void FlavourAction()
    {
        Debug.Log("폭발");
    }
}
