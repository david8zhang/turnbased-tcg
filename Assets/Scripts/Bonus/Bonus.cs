using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Bonus
{
    public struct IngredientBonus
    {
        public Ingredient ing;
        public int bonus;
    }

    public abstract IngredientBonus[] GetBonusIngredients(IngredientPool ingPool);

    public List<Ingredient> FilterIngredientByFlavorType(Ingredient[] ingredients, HashSet<string> flavorTypes)
    {
        List<Ingredient> filtered = new List<Ingredient>();
        foreach (Ingredient ing in ingredients)
        {
            if (flavorTypes.Contains(ing.flavorType))
            {
                filtered.Add(ing);
            }
        }
        return filtered;
    }
}
