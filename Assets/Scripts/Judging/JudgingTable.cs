using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JudgingTable : MonoBehaviour
{
    [SerializeField]
    DishToJudge dishPrefab;

    Combiner.RecipeWithRating playerRecipe;
    Combiner.RecipeWithRating enemyRecipe;

    DishToJudge playerDishToJudge;
    DishToJudge enemyDishToJudge;

    public void Start()
    {
        SetDishes();
        StartCoroutine(CountPoints());
    }

    public void SetDishes()
    {
        playerRecipe = PersistentState.Instance.playerRecipe;
        enemyRecipe = PersistentState.Instance.enemyRecipe;

        playerDishToJudge = Instantiate(dishPrefab, transform);
        playerDishToJudge.CreateFromRecipe(playerRecipe);

        enemyDishToJudge = Instantiate(dishPrefab, transform);
        enemyDishToJudge.CreateFromRecipe(enemyRecipe);
    }

    public void GoToRound()
    {
        SceneManager.LoadScene("Round");
    }

    public int CalculatePoints(Combiner.RecipeWithRating rtt)
    {
        Recipe recipe = rtt.recipe;
        Bonus.IngredientBonus[] bonusIngredients = PersistentState.Instance.bonusIngredients;
        int bonusPoints = RoundBonuses.GetBonusPoints(rtt.actualIngredients, bonusIngredients);
        int totalPoints = recipe.points + (rtt.rating * recipe.ratingMultiplier) + bonusPoints;
        return totalPoints;
    }

    void AddPoints(string keyword, int points)
    {
        int currScore = keyword == "PLAYER" ? PersistentState.Instance.playerScore : PersistentState.Instance.enemyScore;
        currScore += points;
        if (keyword == "PLAYER")
            PersistentState.Instance.playerScore = currScore;
        else
            PersistentState.Instance.enemyScore = currScore;
    }

    IEnumerator CountPoints()
    {
        yield return new WaitForSeconds(1f);
        int playerPoints = CalculatePoints(playerRecipe);
        int enemyPoints = CalculatePoints(enemyRecipe);
        playerDishToJudge.ShowPoints(playerPoints);
        enemyDishToJudge.ShowPoints(enemyPoints);

        AddPoints("PLAYER", playerPoints);
        AddPoints("ENEMY", enemyPoints);

        yield return new WaitForSeconds(1f);
        if (playerPoints > enemyPoints)
        {
            playerDishToJudge.SetWinner();
        } else if (enemyPoints > playerPoints)
        {
            enemyDishToJudge.SetWinner();
        }
    }
}
