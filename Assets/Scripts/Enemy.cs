using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BasePlayer
{
    public void Awake()
    {
        keyword = "ENEMY";
    }

    public override void Start()
    {
        SetScore(keyword);
        base.Start();
    }
    public IEnumerator StartTurn()
    {
        isTurn = true;
        base.DrawCards();
        yield return new WaitForSeconds(1f);

        List<Card> cards = GetCardsToPlay();
        yield return StartCoroutine(PlayCards(cards));
        StartCoroutine(gameManager.EndEnemyTurn());
    }

    public List<Card> GetCardsToPlay()
    {
        List<Card> cardsToPlay = new List<Card>();
        int numCardsToPlay = Mathf.Min(2, hand.Count);
        for (int i = 0; i < numCardsToPlay; i++) {
            IngredientCard cardToPlay = (IngredientCard)GetCardToPlay();
            RemoveCardFromHand(cardToPlay.cardId);
            cardsToPlay.Add(cardToPlay);
        }
        return cardsToPlay;
    }

    public Card GetCardToPlay()
    {
        List<Card> playableCards = new List<Card>();
        foreach (IngredientCard c in hand)
        {
            if (c.GetCardCost() <= heatBar.HeatAmount)
            {
                playableCards.Add(c);
            }
        }
        int randIndex = Random.Range(0, playableCards.Count);
        IngredientCard cardToPlay = (IngredientCard)playableCards[randIndex];
        heatBar.SubtractHeat(cardToPlay.GetCardCost());
        return cardToPlay;
    }

    public void RemoveCardFromHand(int cardId)
    {
        List<Card> newHand = new List<Card>();
        foreach (IngredientCard c in hand)
        {
            if (c.cardId != cardId)
            {
                newHand.Add(c);
            }
        }
        hand = newHand;
    }

    public IEnumerator PlayCards(List<Card> cards)
    {
        List<Card> cardsToDestroy = new List<Card>();
        for (int i = 0; i < cards.Count; i++)
        {
            IngredientCard c = (IngredientCard)cards[i];
            gameManager.PlayCard(c, "enemy");
            cardsToDestroy.Add(c);
        }
        for (int i = 0; i < cardsToDestroy.Count; i++)
        {
            IngredientCard c = (IngredientCard)cardsToDestroy[i];
            if (c)
            {
                Destroy(c.gameObject);
            }
        }
        yield return new WaitForSeconds(1f);
    }
}
