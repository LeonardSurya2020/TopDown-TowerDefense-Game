using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject gameOverUI;



    public void PauseMenu()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void AboutMenu()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

}
