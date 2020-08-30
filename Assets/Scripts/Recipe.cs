﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    public string recipeName;
    public int rating;
    public Ingredient[] coreIngredients;
    public Ingredient[] enhancements;
    public Sprite image;
}
