using UnityEngine;

/// <summary>
/// rollType���� RollSo�� ������ �ش�.
/// </summary>
public class RecipeRoll : MonoBehaviour
{
    public static RecipeRoll instance;
    public RollSo[] recipeRoll;

    void Awake()
    {
        instance = this;
    }
    public RollSo GetRollSo(Roll.rollType _rollType)
    {
        foreach (var _recipe in recipeRoll)
        {
            if (_rollType == _recipe.rollType)
            {
                return _recipe;
            }
        }
        return null;
    }
}
