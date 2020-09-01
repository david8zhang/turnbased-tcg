using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DishCard : Card
{
    public Text dishName;
    public Text ratingLabel;
    public Image image;

    public void CreateFromRecipe(Recipe recipe, int rating)
    {
        dishName.text = recipe.recipeName;
        ratingLabel.text = rating.ToString();
        image.sprite = recipe.image;
    }
}
