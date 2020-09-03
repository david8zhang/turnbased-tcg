using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlavorSprites : MonoBehaviour
{
    public Sprite sourSprite;
    public Sprite sweetSprite;
    public Sprite spicySprite;
    public Sprite noneSprite;
    public Sprite savorySprite;
    public Sprite saltySprite;

    public Sprite GetSpriteForFlavorType(string flavorType)
    {
        switch (flavorType.ToLower())
        {
            case "salty":
                {
                    return saltySprite;
                }
            case "sweet":
                {
                    return sweetSprite;
                }
            case "sour":
                {
                    return sourSprite;
                }
            case "savory":
                {
                    return savorySprite;
                }
            case "none":
                {
                    return noneSprite;
                }
            case "spicy":
                {
                    return spicySprite;
                }
            default:
                return null;
        }
    }
}
