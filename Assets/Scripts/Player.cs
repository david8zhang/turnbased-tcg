using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : BasePlayer
{
    public bool isTurn;

    // Start is called before the first frame update
    public override void Start()
    {
        isTurn = true;
        GenerateRandomDeck();
        InitHand();
    }

    public void StartTurn()
    {
        base.DrawCards();
    }

    internal override Card CreateCard(Ingredient ing, Vector3 position)
    {
        Card card = base.CreateCard(ing, position);
        Button button = card.GetComponent<Button>();
        button.onClick.AddListener(() => SelectCard(card.cardId));
        return card;
    }

    internal override void InitHand()
    {
        Vector3 pos = handLayout.position;
        for (int i = 0; i < numCardsInHand; i++)
        {
            Ingredient ing = deck.Pop();
            Card card = Instantiate(cardPrefab, pos, Quaternion.identity);
            Button button = card.GetComponent<Button>();

            button.onClick.AddListener(() => SelectCard(card.cardId));
            card.transform.SetParent(handLayout, false);
            card.SetCardInfo(ing);
            hand.Add(card);
        }
    }

    void RemoveSelectedCard()
    {
        if (selectedCardId == -1)
        {
            return;
        }
        Card cardToRemove = null;
        int indexToRemove = -1;
        for (int i = 0; i < hand.Count; i++)
        {
            Card c = (Card)hand[i];
            if (c.cardId == selectedCardId)
            {
                cardToRemove = c;
                indexToRemove = i;
            }
        }
        Destroy(cardToRemove.gameObject);
        hand.RemoveAt(indexToRemove);
        selectedCardId = -1;
    }

    void SelectCard(int cardId)
    {
        if (!isTurn)
        {
            return;
        }
        Card oldSelectedCard = GetCardById(selectedCardId);
        if (oldSelectedCard)
        {
            oldSelectedCard.Deselect();
        }
        selectedCardId = cardId;
        Card newSelectedCard = GetCardById(selectedCardId);
        newSelectedCard.Select();
    }

    public void PlayCard()
    {
        if (selectedCardId == -1)
        {
            return;
        }
        Card selectedCard = GetCardById(selectedCardId);
        selectedCard.Deselect();
        gameManager.PlayCard(selectedCard, "player");
        RemoveSelectedCard();
    }
}
