using System;
using System.Linq;
using System.Collections.Generic;

public class MainCourseBonus : Bonus
{
    int numBonuses = 3;

    public override IngredientBonus[] GetBonusIngredients(IngredientPool ingredientPool)
    {
        IngredientBonus[] ingredientBonuses = new IngredientBonus[numBonuses];

        HashSet<string> flavorTypes = new HashSet<string>()
        {
            "savory",
            "salty",
            "none"
        };
        List<Ingredient> filteredIngredients = FilterIngredientByFlavorType(ingredientPool.ingredients, flavorTypes);
        List<int> indices = Enumerable.Range(0, filteredIngredients.Count).ToList();
        Random random = new Random();
        for (int i = 0; i < numBonuses; i++)
        {
            int randIndex = random.Next(0, indices.Count);
            Ingredient randIngredient = filteredIngredients[indices[randIndex]];
            ingredientBonuses[i] = new IngredientBonus
            {
                ing = randIngredient,
                bonus = 8
            };
            indices.RemoveAt(randIndex);
        }
        return ingredientBonuses;
    }
}
