﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfPlay : MonoBehaviour
{
    public ArrayList cardsInPlay;

    private void Start()
    {
        cardsInPlay = new ArrayList();
    }

    public void PlayCard(Card card)
    {
        if (card)
        {
            Card clone = Instantiate(card, card.transform.position, Quaternion.identity) as Card;
            clone.Scale(0.9f);
            clone.transform.SetParent(transform, false);
            cardsInPlay.Add(clone);
        }
    }

    public Ingredient[] GetIngredientsInPlay()
    {
        Ingredient[] ingredients = new Ingredient[cardsInPlay.Count];
        for (int i = 0; i < cardsInPlay.Count; i++)
        {
            Card c = (Card)cardsInPlay[i];
            ingredients[i] = c.ingRef;
        }
        return ingredients;
    }
}
