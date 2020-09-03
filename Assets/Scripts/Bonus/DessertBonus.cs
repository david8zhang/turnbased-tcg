using System;
using System.Linq;
using System.Collections.Generic;

public class DessertBonus : Bonus
{
    int numBonuses = 2;

    public override IngredientBonus[] GetBonusIngredients(IngredientPool ingredientPool)
    {
        IngredientBonus[] ingredientBonuses = new IngredientBonus[numBonuses];

        HashSet<string> flavorTypes = new HashSet<string>()
        {
            "sour",
            "sweet"
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
                bonus = 5
            };
            indices.RemoveAt(randIndex);
        }
        return ingredientBonuses;
    }
}
