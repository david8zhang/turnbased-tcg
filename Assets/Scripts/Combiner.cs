using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combiner : MonoBehaviour
{
    public Recipe[] recipePool;
    public Recipe defaultRecipe;
    public DishCard dishCardPrefab;

    public struct RecipeWithRating
    {
        public Recipe recipe;
        public int rating;
    }

    public RecipeWithRating CombineIngredients(Ingredient[] ingredients)
    {
        foreach (Recipe r in recipePool) {
            HashSet<string> coreIngNames = GetNamesOfIngredients(r.coreIngredients);
            HashSet<string> combinedIngNames = GetNamesOfIngredients(ingredients);
            if (coreIngNames.IsSubsetOf(combinedIngNames))
            {
                // Check how many stars based on what enhancements were added
                combinedIngNames.ExceptWith(coreIngNames);
                HashSet<string> enhancementNames = GetNamesOfIngredients(r.enhancements);
                combinedIngNames.IntersectWith(enhancementNames);
                int numWrongIngs = Mathf.Abs(combinedIngNames.Count - r.enhancements.Length);
                //r.rating = Mathf.Max(5 - numWrongIngs, 1);

                return new RecipeWithRating
                {
                    recipe = r,
                    rating = Mathf.Max(5 - numWrongIngs, 1)
                };
            }
        }
        //defaultRecipe.rating = 0;
        return new RecipeWithRating
        {
            recipe = defaultRecipe,
            rating = 0
        };
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

    public DishCard CreateDish(Recipe r, int rating)
    {
        DishCard dishCard = Instantiate(dishCardPrefab, Vector3.zero, Quaternion.identity);
        dishCard.CreateFromRecipe(r, rating);
        return dishCard;
    }
}
