using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combiner : MonoBehaviour
{
    public Recipe[] recipePool;
    public Recipe defaultRecipe;

    public Recipe CombineIngredients(Ingredient[] ingredients)
    {
        foreach (Recipe r in recipePool) {
            HashSet<string> coreIngNames = GetNamesOfIngredients(r.coreIngredients);
            HashSet<string> combinedIngNames = GetNamesOfIngredients(ingredients);
            if (coreIngNames.IsSubsetOf(combinedIngNames))
            {
                // Check how many stars based on what enhancements were added
                combinedIngNames.ExceptWith(coreIngNames);
                HashSet<string> enhancementNames = GetNamesOfIngredients(r.enhancements);
                int numWrongIngs = Mathf.Abs(combinedIngNames.Count - r.enhancements.Length);
                r.rating = Mathf.Max(5 - numWrongIngs, 1);
                return r;
            }
        }
        defaultRecipe.rating = 0;
        return defaultRecipe;
    }

    public HashSet<string> GetNamesOfIngredients(Ingredient[] ing)
    {
        HashSet<string> names = new HashSet<string>();
        for (int i = 0; i < ing.Length; i++)
        {
            names.Add(ing[i].ingredientName);
        }
        return names;
    }
}
