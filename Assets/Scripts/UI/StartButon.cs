using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButon : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Village");
        Time.timeScale = 1;
    }
    public void Retry()
    {
        SceneManager.LoadScene("Boss_GoblinKing");
        Time.timeScale = 1;
    }
}
