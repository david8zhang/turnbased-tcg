using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JudgingTable : MonoBehaviour
{
    [SerializeField]
    DishToJudge dishPrefab;

    GameManager.RecipeToTransfer playerRecipe;
    GameManager.RecipeToTransfer enemyRecipe;

    DishToJudge playerDishToJudge;
    DishToJudge enemyDishToJudge;

    public void Start()
    {
        SetDishes();
        StartCoroutine(CountPoints());
    }

    public void SetDishes()
    {
        string playerRecipeData = PlayerPrefs.GetString("player_recipe");
        string enemyRecipeData = PlayerPrefs.GetString("enemy_recipe");
        playerRecipe = JsonUtility.FromJson<GameManager.RecipeToTransfer>(playerRecipeData);
        enemyRecipe = JsonUtility.FromJson<GameManager.RecipeToTransfer>(enemyRecipeData);

        playerDishToJudge = Instantiate(dishPrefab, transform);
        playerDishToJudge.CreateFromRecipe(playerRecipe);

        enemyDishToJudge = Instantiate(dishPrefab, transform);
        enemyDishToJudge.CreateFromRecipe(enemyRecipe);
    }

    public void GoToRound()
    {
        
        SceneManager.LoadScene("Round");
    }

    public int CalculatePoints(GameManager.RecipeToTransfer rtt)
    {
        int totalPoints = rtt.points + (rtt.rating * rtt.ratingMultiplier);
        return totalPoints;
    }

    void AddPoints(string keyword, int points)
    {
        int currPoints = PlayerPrefs.GetInt(keyword + "_score");
        PlayerPrefs.SetInt(keyword + "_score", currPoints + points);
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
