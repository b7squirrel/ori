using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] float lifeTime;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
