using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    Card cardPrefab;

    [SerializeField]
    IngredientPool ingredientPool;

    [SerializeField]
    Transform playerHandLayout;

    Card[] hand;
    Stack<Ingredient> deck;
    int numCardsInDeck = 30;
    int numCardsInHand = 5;


    // Start is called before the first frame update
    void Start()
    {
        GenerateRandomDeck();
        InitHand();
    }

    void GenerateRandomDeck()
    {
        deck = new Stack<Ingredient>();
        for (int i = 0; i < numCardsInDeck; i++)
        {
            deck.Push(ingredientPool.GetRandomIngredient());
        }
    }

    void InitHand()
    {
        Vector3 pos = playerHandLayout.position;
        hand = new Card[numCardsInHand];
        for (int i = 0; i < numCardsInHand; i++)
        {
            Ingredient ing = deck.Pop();
            Card card = Instantiate(cardPrefab, pos, Quaternion.identity) as Card;
            card.transform.SetParent(playerHandLayout);
            card.SetCardInfo(ing);
            hand[i] = card;
        }
    }
}
