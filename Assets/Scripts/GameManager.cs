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
        StartCoroutine(enemy.StartTurn());
        player.isTurn = false;
    }

    public void EndEnemyTurn()
    {
        player.StartTurn();
        player.isTurn = true;
    }
}
