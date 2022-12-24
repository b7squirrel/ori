using UnityEngine;

[CreateAssetMenu(fileName = "newRollSo", menuName = "Data/Roll/RollSo")]
public class RollSo : ScriptableObject
{
    public Roll.rollType rollType;
    public GameObject rollPrefab;
    public Vector2 velocity;
    //public float height;
    //public float mass;
    //public float gravity;
    //public float timeForExplosion;
    //public float durationAsRoll;
    //public Vector2 shootVelocity;
    //public GameObject fragmentPrefab;  // Á×À½ ÀÌÆåÆ®
}
