using UnityEngine;

public class FlavourGen : MonoBehaviour
{
    public float FlavourLife { get; set; }
    public bool IsFlying { get; set; }

    [field : SerializeField]
    public Flavour.flavourType FlavourType { get; private set; }

    void Update()
    {
        if (!IsFlying)
            return;
        if (FlavourLife > 0)
        {
            FlavourLife -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
