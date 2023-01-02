using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPhysics
{
    void AddHitRollPhysics(float mouseDir, float movementInputDir, float playerFacingDir, float playerVelocity);
    void AddDumpRollPhysics();
}
