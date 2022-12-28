using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject EndPanel;
    public GameObject GameOverText;
    public GameObject VictoryText;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Village();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Forest();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GoblinKing();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Menu();
        }
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Village()
    {
        SceneManager.LoadScene("Village");
    }
    public void Forest()
    {
        SceneManager.LoadScene("Forest");
    }
    public void GoblinKing()
    {
        SceneManager.LoadScene("Boss_GoblinKing");
    }
    public void GameOver()
    {
        pause();
        EndPanel.SetActive(true);
        GameOverText.SetActive(true);
    }
    public void Victory()
    {
        pause();
        EndPanel.SetActive(true);
        VictoryText.SetActive(true);
    }

    public void pause()
    {
        Time.timeScale = 0;
    }

    public void resume()
    {
        Time.timeScale = 1;
    }
}
