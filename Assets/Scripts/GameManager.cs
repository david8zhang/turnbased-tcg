using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Player player;

    [SerializeField]
    Enemy enemy;

    [SerializeField]
    FieldOfPlay playerField;

    [SerializeField]
    FieldOfPlay enemyField;

    [SerializeField]
    Combiner combiner;

    public void PlayCard(Card card, string side)
    {
        if (side == "player")
        {
            playerField.PlayCard(card);
        } else
        {
            enemyField.PlayCard(card);
        }

    }

    public void EndPlayerTurn()
    {
        Recipe r = combiner.CombineIngredients(playerField.GetIngredientsInPlay());
        Debug.Log(r.recipeName + ": " + r.rating);
        StartCoroutine(enemy.StartTurn());
        player.isTurn = false;
    }

    public void EndEnemyTurn()
    {
        player.StartTurn();
        player.isTurn = true;
    }
}
