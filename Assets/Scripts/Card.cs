using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{

    public Text cardName;
    public Text cardCost;
    public Image cardImage;
    public Image cardType;

    public FlavorSprites flavorSprites;


    public void SetCardInfo(Ingredient ingredientRef)
    {
        cardName.text = ingredientRef.ingredientName;
        cardCost.text = ingredientRef.cost.ToString();
        cardImage.sprite = ingredientRef.image;
        cardType.sprite = flavorSprites.GetSpriteForFlavorType(ingredientRef.flavorType);
    }
}
