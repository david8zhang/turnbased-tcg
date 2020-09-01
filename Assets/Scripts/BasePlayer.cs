using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BasePlayer : MonoBehaviour
{
    [SerializeField]
    internal Score scoreObj;

    [SerializeField]
    internal IngredientCard cardPrefab;

    [SerializeField]
    internal IngredientPool ingredientPool;

    [SerializeField]
    internal Transform handLayout;

    [SerializeField]
    internal GameManager gameManager;

    internal List<Card> hand = new List<Card>();
    internal Stack<Ingredient> deck;
    internal int numCardsInDeck = 30;
    internal int selectedCardId = -1;
    internal int numCardsToDraw = 2;
    internal int numCardsInHand = 5;

    internal static string keyword = "BASE";

    [System.Serializable]
    public struct SavedDeck
    {
        public Ingredient[] deck;
    }

    [System.Serializable]
    public struct SavedHand
    {
        public Ingredient[] hand;
    }

    public virtual void Start()
    {
        Stack<Ingredient> sd = GetSavedDeck();
        Ingredient[] sh = GetSavedHand();
        if (sd == null)
        {
            GenerateRandomDeck();
        }
        else
        {
            deck = sd;
        }
        if (sh == null)
        {
            InitHand();
        }
        else
        {
            InitSavedHand(sh);
        }
    }

    public virtual void SetScore(string keyword)
    {
        int score = PlayerPrefs.GetInt(keyword + "_score");
        scoreObj.SetScore(score);
    }

    internal void SaveDeck()
    {
        SavedDeck sd = new SavedDeck { deck = deck.ToArray() };
        string savedDeck = JsonUtility.ToJson(sd);
        PlayerPrefs.SetString(keyword + "_deck", savedDeck);
    }

    internal void SaveHand()
    {
        Ingredient[] ingredientsInHand = new Ingredient[hand.Count];
        for (int i = 0; i < hand.Count; i++)
        {
            IngredientCard card = (IngredientCard)(hand[i]);
            ingredientsInHand[i] = card.ingRef;
        }
        SavedHand sh = new SavedHand { hand = ingredientsInHand };
        string savedHand = JsonUtility.ToJson(sh);
        PlayerPrefs.SetString(keyword + "_hand", savedHand);
    }

    internal Stack<Ingredient> GetSavedDeck()
    {
        string data = PlayerPrefs.GetString(keyword + "_deck");
        if (data != "")
        {
            SavedDeck savedDeck = JsonUtility.FromJson<SavedDeck>(data);
            Ingredient[] ingredients = savedDeck.deck;
            Stack<Ingredient> deckStack = new Stack<Ingredient>();
            for (int i = ingredients.Length - 1; i >= 0; i--)
            {
                deckStack.Push(ingredients[i]);
            }
            return deckStack;
        } else
        {
            return null;
        }
    }

    internal Ingredient[] GetSavedHand()
    {
        string data = PlayerPrefs.GetString(keyword + "_hand");
        if (data != "")
        {
            SavedHand savedHand = JsonUtility.FromJson<SavedHand>(data);
            return savedHand.hand;
        }
        else
        {
            return null;
        }
    }
   
    internal void GenerateRandomDeck()
    {
        deck = new Stack<Ingredient>();
        for (int i = 0; i < numCardsInDeck; i++)
        {
            deck.Push(ingredientPool.GetRandomIngredient());
        }
    }

    public virtual void DrawCards()
    {
        Vector3 pos = handLayout.position;
        for (int i = 0; i < numCardsToDraw; i++)
        {
            IngredientCard card = CreateCard(deck.Pop(), pos);
            hand.Add(card);
        }
    }

    internal virtual void InitHand()
    {
        Vector3 pos = handLayout.position;
        for (int i = 0; i < numCardsInHand; i++)
        {
            Card card = CreateCard(deck.Pop(), pos);
            hand.Add(card);
        }
    }

    internal virtual void InitSavedHand(Ingredient[] savedHand)
    {
        Vector3 pos = handLayout.position;
        for (int i = 0; i < savedHand.Length; i++)
        {
            Card card = CreateCard(savedHand[i], pos);
            hand.Add(card);
        }
    }

    public IngredientCard GetCardById(int cardId)
    {
        if (cardId != -1)
        {
            for (int i = 0; i < hand.Count; i++)
            {
                IngredientCard c = (IngredientCard)hand[i];
                if (c.cardId == cardId)
                {
                    return c;
                }
            }
        }
        return null;

    }

    internal virtual IngredientCard CreateCard(Ingredient ing, Vector3 position)
    {
        IngredientCard card = Instantiate(cardPrefab, position, Quaternion.identity);
        card.transform.SetParent(handLayout, false);
        card.SetCardInfo(ing);
        return card;
    }
}
