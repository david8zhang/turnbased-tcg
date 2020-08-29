using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPool : MonoBehaviour
{
    public Ingredient[] ingredients;

    public Ingredient GetRandomIngredient()
    {
        int randomIndex = Random.Range(0, ingredients.Length);
        return ingredients[randomIndex];
    }
}
