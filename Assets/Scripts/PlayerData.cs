using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newPlayerData", menuName ="Data/Player/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity;

    [Header("Jump State")]
    public float jumpVelocity;
    public int amountOfJumps;

    [Header("In Air State")]
    public float coyoteTime;

    [Header("Check Variables")]
    public float groundCheckRadius;
    public LayerMask whatIsGround;
}
