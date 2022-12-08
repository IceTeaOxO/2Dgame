using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButon : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Boss_GoblinKing");
    }
}
