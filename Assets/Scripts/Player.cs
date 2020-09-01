using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : BasePlayer
{
    public bool isTurn;
    public static new string keyword = "PLAYER";

    // Start is called before the first frame update
    public override void Start()
    {
        SetScore(keyword);
        base.Start();
        isTurn = true;
        AttachClickListenersToCardsInHand();
    }

    public void StartTurn()
    {
        base.DrawCards();
        SaveDeck();
        SaveHand();
    }

    internal override IngredientCard CreateCard(Ingredient ing, Vector3 position)
    {
        IngredientCard card = base.CreateCard(ing, position);
        Button button = card.GetComponent<Button>();
        button.onClick.AddListener(() => SelectCard(card.cardId));
        return card;
    }

    internal void AttachClickListenersToCardsInHand()
    {
        foreach (IngredientCard c in hand)
        {
            Button button = c.GetComponent<Button>();
            button.onClick.AddListener(() => SelectCard(c.cardId));
        }
    }

    void RemoveSelectedCard()
    {
        if (selectedCardId == -1)
        {
            return;
        }
        IngredientCard cardToRemove = null;
        int indexToRemove = -1;
        for (int i = 0; i < hand.Count; i++)
        {
            IngredientCard c = (IngredientCard)hand[i];
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
        IngredientCard oldSelectedCard = GetCardById(selectedCardId);
        if (oldSelectedCard)
        {
            oldSelectedCard.Deselect();
        }
        selectedCardId = cardId;
        IngredientCard newSelectedCard = GetCardById(selectedCardId);
        newSelectedCard.Select();
    }

    public void PlayCard()
    {
        if (selectedCardId == -1)
        {
            return;
        }
        IngredientCard selectedCard = GetCardById(selectedCardId);
        selectedCard.Deselect();
        gameManager.PlayCard(selectedCard, "player");
        RemoveSelectedCard();

        // Save hand and deck for next round
        SaveDeck();
        SaveHand();
    }
}
