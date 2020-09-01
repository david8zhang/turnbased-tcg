using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{

    [SerializeField]
    Text winLabel;

    // Start is called before the first frame update
    void Start()
    {
        int playerPoints = PlayerPrefs.GetInt("PLAYER_score");
        int enemyPoints = PlayerPrefs.GetInt("ENEMY_points");

        if (playerPoints >= 150)
        {
            winLabel.text = "Player wins!";
        }
        else if (enemyPoints >= 150)
        {
            winLabel.text = "Enemy wins!";
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
