using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundBonuses : MonoBehaviour
{
    [SerializeField]
    IngredientPool ingredientPool;

    public Bonus.IngredientBonus[] GetIngredientBonuses(int currRound)
    {
        Bonus bonus = null;
        if (currRound >= 1 && currRound <= 3)
        {
            bonus = new AppetizerBonus();
        }
        if (currRound >= 4 && currRound <= 6)
        {
            bonus = new MainCourseBonus();
        }
        if (currRound >= 7 && currRound <= 8)
        {
            bonus = new DessertBonus();
        }
        return bonus.GetBonusIngredients(ingredientPool);
    }

    public static int GetBonusPoints(Ingredient[] ingredients, Bonus.IngredientBonus[] ingredientBonuses)
    {
        int bonusScore = 0;
        foreach (Ingredient ing in ingredients)
        {
            Bonus.IngredientBonus matching = GetMatchingBonus(ingredientBonuses, ing);
            bonusScore += matching.bonus;
        }
        return bonusScore;
    }

    static Bonus.IngredientBonus GetMatchingBonus(Bonus.IngredientBonus[] ingBonuses, Ingredient toCheck)
    {
        foreach (Bonus.IngredientBonus ib in ingBonuses)
        {
            if (toCheck.ingredientName == ib.ing.ingredientName)
            {
                return ib;
            }
        }
        return new Bonus.IngredientBonus
        {
            bonus = 0,
            ing = null
        };
    }
}
