using UnityEngine;

public class RecipeFlavour : MonoBehaviour
{
    public static RecipeFlavour instance;
    public FlavourSo[] recipeFlavour;

    void Awake()
    {
        instance = this;
    }
    public FlavourSo GetFlavourSo(Flavour.flavourType flavourType)
    {
        foreach (var _recipe in recipeFlavour)
        {
            if (flavourType == _recipe.flavourType)
            {
                return _recipe;
            }
        }
        return null;
    }
}
