using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Player player;

    [SerializeField]
    Enemy enemy;

    [SerializeField]
    FieldOfPlay playerField;

    [SerializeField]
    FieldOfPlay enemyField;

    [SerializeField]
    Combiner combiner;

    Combiner.RecipeWithRating enemyRecipe;
    Combiner.RecipeWithRating playerRecipe;

    [System.Serializable]
    public struct RecipeToTransfer
    {
        public string recipeName;
        public int rating;
        public Sprite image;
        public int points;
        public int ratingMultiplier;
    }

    public void PlayCard(Card card, string side)
    {
        if (side == "player")
        {
            playerField.PlayCard(card);
        } else
        {
            enemyField.PlayCard(card);
        }

    }

    Combiner.RecipeWithRating ProcessIngredients(FieldOfPlay field)
    {
        Combiner.RecipeWithRating r = combiner.CombineIngredients(field.GetIngredientsInPlay());
        DishCard dishCard = combiner.CreateDish(r.recipe, r.rating);
        field.ClearCards();
        field.PlayCard(dishCard);
        return r;
    }


    public void EndPlayerTurn()
    {
        enemyRecipe = ProcessIngredients(playerField);
        StartCoroutine(enemy.StartTurn());
        player.isTurn = false;
    }

    public IEnumerator EndEnemyTurn()
    {
        playerRecipe = ProcessIngredients(enemyField);
        player.StartTurn();
        yield return new WaitForSeconds(1.5f);
        GoToJudgingScene();
    }

    public void GoToJudgingScene()
    {
        // serialize recipes from both sides
        RecipeToTransfer playerRecipeToTransfer = new RecipeToTransfer
        {
            recipeName = playerRecipe.recipe.recipeName,
            rating = playerRecipe.rating,
            image = playerRecipe.recipe.image,
            points = playerRecipe.recipe.points,
            ratingMultiplier = playerRecipe.recipe.ratingMultiplier
        };

        RecipeToTransfer enemyRecipeToTransfer = new RecipeToTransfer
        {
            recipeName = enemyRecipe.recipe.recipeName,
            rating = enemyRecipe.rating,
            image = enemyRecipe.recipe.image,
            points = enemyRecipe.recipe.points,
            ratingMultiplier = enemyRecipe.recipe.ratingMultiplier
        };

        PlayerPrefs.SetString("player_recipe", JsonUtility.ToJson(playerRecipeToTransfer));
        PlayerPrefs.SetString("enemy_recipe", JsonUtility.ToJson(enemyRecipeToTransfer));
        SceneManager.LoadScene("Judging");
    }
}
