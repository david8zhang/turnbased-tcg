using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BasePlayer
{
    public IEnumerator StartTurn()
    {
        base.DrawCards();
        yield return new WaitForSeconds(1f);

        ArrayList cards = GetCardsToPlay();
        yield return StartCoroutine(PlayCards(cards));
        gameManager.EndEnemyTurn();
    }

    public ArrayList GetCardsToPlay()
    {
        ArrayList cardsToPlay = new ArrayList();
        int numCardsToPlay = Mathf.Min(2, hand.Count);
        for (int i = 0; i < numCardsToPlay; i++) {
            int randIndex = Random.Range(0, hand.Count);
            Card c = (Card)hand[randIndex];
            hand.Remove(randIndex);
            cardsToPlay.Add(c.cardId);
        }
        return cardsToPlay;
    }

    public IEnumerator PlayCards(ArrayList cards)
    {
        ArrayList cardsToDestroy = new ArrayList();
        for (int i = 0; i < cards.Count; i++)
        {
            int cardId = (int)cards[i];
            Card c = GetCardById(cardId);
            gameManager.PlayCard(c, "enemy");
            cardsToDestroy.Add(c);
        }
        for (int i = 0; i < cardsToDestroy.Count; i++)
        {
            Card c = (Card)cardsToDestroy[i];
            if (c)
            {
                Destroy(c.gameObject);
            }
        }
        yield return new WaitForSeconds(2f);
    }
}
