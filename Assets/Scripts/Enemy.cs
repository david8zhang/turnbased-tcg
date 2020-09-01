using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BasePlayer
{
    public IEnumerator StartTurn()
    {
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
            int randIndex = Random.Range(0, hand.Count);
            IngredientCard c = (IngredientCard)hand[randIndex];
            cardsToPlay.Add(c);
            hand.RemoveAt(randIndex);
        }
        return cardsToPlay;
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
            Debug.Log(c);
            if (c)
            {
                Destroy(c.gameObject);
            }
        }
        yield return new WaitForSeconds(1f);
    }
}
