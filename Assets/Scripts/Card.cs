﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Image cardBorder;
    public Text cardName;
    public Text cardCost;
    public Image cardImage;
    public Image cardType;

    public int cardId;

    public FlavorSprites flavorSprites;

    public void SetCardInfo(Ingredient ingredientRef)
    {
        cardId = gameObject.GetInstanceID();
        cardName.text = ingredientRef.ingredientName;
        cardCost.text = ingredientRef.cost.ToString();
        cardImage.sprite = ingredientRef.image;
        cardType.sprite = flavorSprites.GetSpriteForFlavorType(ingredientRef.flavorType);
    }

    public void Select()
    {
        Scale(0.9f);
        HighlightBorder();
        Move(new Vector3(0, 110, 0));
    }

    public void Deselect()
    {
        Scale(0.7f);
        DeHighlightBorder();
        Move(new Vector3(0, -110, 0));
    }

    public void HighlightBorder()
    {
        cardBorder.color = new Color32(127, 255, 0, 255);
    }

    public void DeHighlightBorder()
    {
        cardBorder.color = new Color32(0, 0, 0, 255);
    }


    public void Scale(float percentage)
    {
        Vector3 newScale = Vector3.one * percentage;
        transform.localScale = newScale;
    }

    public void Move(Vector3 moveAmount)
    {
        transform.position += moveAmount;
    }
}
