using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject gameOver;

    public static GameController instance;

    private void Start()
    {
        instance = this;
    }

    public void showGameOver()
    {
        Time.timeScale = 0f;
        gameOver.SetActive(true);
    }

    public void restartGame(string lvlName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(lvlName);
    }
}
