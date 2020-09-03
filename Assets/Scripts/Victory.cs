using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{

    [SerializeField]
    Text winLabel;

    [SerializeField]
    Text playerFinalScore;

    [SerializeField]
    Text enemyFinalScore;

    // Start is called before the first frame update
    void Start()
    {
        int playerPoints = PersistentState.Instance.playerScore;
        int enemyPoints = PersistentState.Instance.enemyScore;

        if (playerPoints == enemyPoints)
        {
            winLabel.text = "It's a tie!";
        }

        if (playerPoints >= GameManager.winThreshold)
        {
            winLabel.text = "Player wins!";
        }
        else if (enemyPoints >= GameManager.winThreshold)
        {
            winLabel.text = "Enemy wins!";
        } else
        {
            winLabel.text = playerPoints > enemyPoints ? "Player wins!" : "Enemy wins!";
        }

        playerFinalScore.text = playerPoints.ToString();
        enemyFinalScore.text = enemyPoints.ToString();
    }

    public void GoToMainMenu()
    {
        PersistentState.Instance.Clear();
        SceneManager.LoadScene("MainMenu");
    }
}
