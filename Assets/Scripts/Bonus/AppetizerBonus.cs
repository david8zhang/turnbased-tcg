using System;
using System.Linq;
using System.Collections.Generic;

public class AppetizerBonus : Bonus
{
    int numBonuses = 1;

    public override IngredientBonus[] GetBonusIngredients(IngredientPool ingredientPool)
    {
        IngredientBonus[] ingredientBonuses = new IngredientBonus[numBonuses];
        List<int> indices = Enumerable.Range(0, ingredientPool.ingredients.Length).ToList();
        Random random = new Random();
        for (int i = 0; i < numBonuses; i++)
        {
            int randIndex = random.Next(0, indices.Count);
            Ingredient randIngredient = ingredientPool.ingredients[indices[randIndex]];
            ingredientBonuses[i] = new IngredientBonus
            {
                ing = randIngredient,
                bonus = 3
            };
            indices.RemoveAt(randIndex);
        }
        return ingredientBonuses;
    }
}
