using UnityEngine;
using System;
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

    [SerializeField]
    internal HeatBar heatBar;

    internal List<Card> hand = new List<Card>();
    internal Stack<Ingredient> deck;
    internal int numCardsInDeck = 30;
    internal int selectedCardId = -1;
    internal int numCardsToDraw = 2;
    internal int numCardsInHand = 5;
    internal int initialHeatAmount = 3;

    public string keyword = "BASE";

    internal bool isTurn = false;

    [Serializable]
    public struct SavedDeck
    {
        public Ingredient[] deck;
    }

    [Serializable]
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
        if (sh.Length == 0)
        {
            InitHand();
        }
        else
        {
            InitSavedHand(sh);
        }
        InitHeatBar();
    }

    public void InitHeatBar()
    {
        int savedHeat = GetSavedHeat();
        if (savedHeat > 0)
        {
            heatBar.TotalHeatAmount = savedHeat;
        } else
        {
            heatBar.TotalHeatAmount = initialHeatAmount;
        }
        heatBar.ResetHeat();
    }

    public bool CheckCardPlayable(IngredientCard card)
    {
        return card.GetCardCost() <= heatBar.HeatAmount;
    }

    public bool IsDeckEmpty()
    {
        return deck != null && deck.Count == 0;
    }

    public virtual void SetScore()
    {
    }

    internal void SaveDeck()
    {
        PersistentState.Instance.SetDeck(keyword, deck);
    }

    internal void SaveHand()
    {
        Ingredient[] ingredientsInHand = new Ingredient[hand.Count];
        for (int i = 0; i < hand.Count; i++)
        {
            IngredientCard card = (IngredientCard)(hand[i]);
            ingredientsInHand[i] = card.ingRef;
        }
        PersistentState.Instance.SetHand(keyword, ingredientsInHand);
    }

    internal void SaveHeat()
    {
        PersistentState.Instance.SetHeat(keyword, heatBar.TotalHeatAmount);
    }

    internal int GetSavedHeat()
    {
        return PersistentState.Instance.GetHeat(keyword);
    }

    internal Stack<Ingredient> GetSavedDeck()
    {
        return PersistentState.Instance.GetDeck(keyword);
    }

    internal Ingredient[] GetSavedHand()
    {
        return PersistentState.Instance.GetHand(keyword);
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
            if (deck.Count > 0)
            {
                IngredientCard card = CreateCard(deck.Pop(), pos);
                hand.Add(card);
            }
        }
        SaveDeck();
        SaveHand();
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

    public virtual void EndTurn()
    {
        SaveDeck();
        SaveHand();

        // Increase heat amount by 1
        heatBar.TotalHeatAmount += 1;
        heatBar.ResetHeat();
        SaveHeat();
        isTurn = false;
    }

    internal virtual IngredientCard CreateCard(Ingredient ing, Vector3 position)
    {
        IngredientCard card = Instantiate(cardPrefab, position, Quaternion.identity);
        card.transform.SetParent(handLayout, false);
        card.SetCardInfo(ing);
        return card;
    }
}
