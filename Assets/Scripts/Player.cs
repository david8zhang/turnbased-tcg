using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : BasePlayer
{
    public void Awake()
    {
        isTurn = true;
        keyword = "PLAYER";
    }

    // Start is called before the first frame update
    public override void Start()
    {
        SetScore(keyword);
        base.Start();
        AttachClickListenersToCardsInHand();
    }

    public void StartTurn()
    {
        isTurn = true;
        base.DrawCards();
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
        if (!isTurn || !CheckCardPlayable(GetCardById(cardId)))
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

        heatBar.SubtractHeat(selectedCard.GetCardCost());
    }
}
