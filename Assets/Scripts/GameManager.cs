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

    [SerializeField]
    Text roundLabel;

    [SerializeField]
    RoundBonuses roundBonuses;

    [SerializeField]
    Transform bonusList;

    [SerializeField]
    GameObject bonusText;

    Combiner.RecipeWithRating enemyRecipe;
    Combiner.RecipeWithRating playerRecipe;
    Bonus.IngredientBonus[] bonusIngredients;

    public static readonly int winThreshold = 100;

    int totalRounds = 8;
    int currRound = 1;

    public void Start()
    {
        SetRound();
        if (CheckWinCondition())
        {
            SceneManager.LoadScene("Victory");
        } else
        {
            SetupBonuses();
        }
    }

    void SetupBonuses()
    {
        bonusIngredients = roundBonuses.GetIngredientBonuses(currRound);
        foreach (Bonus.IngredientBonus ib in bonusIngredients)
        {
            GameObject bonusTextClone = Instantiate(bonusText, bonusList);
            bonusTextClone.GetComponent<Text>().text = "Bonus: " + ib.ing.ingredientName;
        }
    }

    public bool CheckWinCondition()
    {
        Debug.Log(currRound);
        int playerPoints = PersistentState.Instance.playerScore;
        int enemyPoints = PersistentState.Instance.enemyScore;
        return playerPoints >= winThreshold || enemyPoints >= winThreshold || currRound > totalRounds;
    }

    public void SetRound()
    {
        int savedRound = PersistentState.Instance.round;
        currRound = savedRound > 0 ? savedRound : 1;
        if (currRound == totalRounds)
        {
            roundLabel.text = "Final Round";
        } else
        {
            roundLabel.text = "Round " + currRound;
        }
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
        // End the player turn
        playerRecipe = ProcessIngredients(playerField);
        player.EndTurn();

        // Start the enemy turn
        StartCoroutine(enemy.StartTurn());
    }

    public IEnumerator EndEnemyTurn()
    {
        // End the enemy turn
        enemyRecipe = ProcessIngredients(enemyField);
        enemy.EndTurn();

        // Start the player's turn (so after judging round it's players turn again)
        player.StartTurn();
        yield return new WaitForSeconds(1.5f);
        GoToJudgingScene();
    }

    public void GoToJudgingScene()
    {
        currRound++;
        PersistentState.Instance.bonusIngredients = bonusIngredients;
        PersistentState.Instance.round = currRound;
        PersistentState.Instance.playerRecipe = playerRecipe;
        PersistentState.Instance.enemyRecipe = enemyRecipe;
        SceneManager.LoadScene("Judging");
    }
}
