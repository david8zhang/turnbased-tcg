using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DishCard : MonoBehaviour
{
    public Text dishName;
    public Text rating;
    public Image image;

    public void CreateFromRecipe(Recipe recipe)
    {
        dishName.text = recipe.recipeName;
        rating.text = recipe.rating.ToString();
        image.sprite = recipe.image;
    }
}
