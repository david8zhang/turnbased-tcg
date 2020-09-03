using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DishToJudge : MonoBehaviour
{
    public Text dishName;
    public Image image;
    public Text rating;
    public Text pointsLabel;
    public GameObject pointsLabelWrapper;
    public Image border;

    public void CreateFromRecipe(Combiner.RecipeWithRating recipeWithRating)
    {
        Recipe recipe = recipeWithRating.recipe;
        dishName.text = recipe.recipeName;
        image.sprite = recipe.image;
        rating.text = recipeWithRating.rating.ToString();

    }

    public void ShowPoints(int points)
    {
        image.gameObject.SetActive(false);
        pointsLabelWrapper.gameObject.SetActive(true);
        pointsLabel.text = points.ToString();
    }


    public void SetWinner()
    {
        border.color = new Color32(0, 255, 0, 255);
        dishName.text = "Winner!";
    }
}
