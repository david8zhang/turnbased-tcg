using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentState : MonoBehaviour
{
    public static PersistentState Instance { get; private set; }

    public Bonus.IngredientBonus[] bonusIngredients;
    public Combiner.RecipeWithRating playerRecipe;
    public Combiner.RecipeWithRating enemyRecipe;

    public int round = 0;
    public int playerScore;
    public int enemyScore;

    public int playerHeat;
    public int enemyHeat;

    public Stack<Ingredient> playerDeck;
    public Stack<Ingredient> enemyDeck;

    public Ingredient[] playerHand;
    public Ingredient[] enemyHand;


    public void Clear()
    {
        round = 0;
        playerScore = 0;
        enemyScore = 0;
        bonusIngredients = null;
        enemyHeat = 3;
        playerHeat = 3;
        playerDeck = null;
        enemyDeck = null;
        playerHand = new Ingredient[0];
        enemyHand = new Ingredient[0];
    }

    public int GetHeat(string keyword)
    {
        return keyword == "PLAYER" ? playerHeat : enemyHeat;
    }

    public void SetHeat(string keyword, int heat)
    {
        if (keyword == "PLAYER")
        {
            playerHeat = heat;
        } else
        {
            enemyHeat = heat;
        }
    }


    public Stack<Ingredient> GetDeck(string keyword)
    {
        return keyword == "PLAYER" ? playerDeck : enemyDeck;
    }

    public void SetDeck(string keyword, Stack<Ingredient> deck)
    {
        if (keyword == "PLAYER")
        {
            playerDeck = deck;
        } else
        {
            enemyDeck = deck;
        }
    }


    public Ingredient[] GetHand(string keyword)
    {
        return keyword == "PLAYER" ? playerHand : enemyHand;
    }

    public void SetHand(string keyword, Ingredient[] hand)
    {
        if (keyword == "PLAYER")
        {
            playerHand = hand;
        }
        else
        {
            enemyHand = hand;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }
}
