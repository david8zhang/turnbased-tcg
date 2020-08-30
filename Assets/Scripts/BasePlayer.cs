using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasePlayer : MonoBehaviour
{

    [SerializeField]
    internal Card cardPrefab;

    [SerializeField]
    internal IngredientPool ingredientPool;

    [SerializeField]
    internal Transform handLayout;

    [SerializeField]
    internal GameManager gameManager;

    internal ArrayList hand;
    internal Stack<Ingredient> deck;
    internal int numCardsInDeck = 30;
    internal int selectedCardId = -1;
    internal int numCardsToDraw = 2;
    internal int numCardsInHand = 5;

    public virtual void Start()
    {
        GenerateRandomDeck();
        InitHand();
    }

    internal void GenerateRandomDeck()
    {
        deck = new Stack<Ingredient>();
        hand = new ArrayList();
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
            Card card = CreateCard(deck.Pop(), pos);
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

    public Card GetCardById(int cardId)
    {
        if (cardId != -1)
        {
            for (int i = 0; i < hand.Count; i++)
            {
                Card c = (Card)hand[i];
                if (c.cardId == cardId)
                {
                    return c;
                }
            }
        }
        return null;

    }

    internal virtual Card CreateCard(Ingredient ing, Vector3 position)
    {
        Card card = Instantiate(cardPrefab, position, Quaternion.identity);
        card.transform.SetParent(handLayout, false);
        card.SetCardInfo(ing);
        return card;
    }
}
