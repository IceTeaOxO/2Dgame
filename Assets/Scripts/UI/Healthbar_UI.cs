using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar_UI : MonoBehaviour
{
    public Image Border;
    public Image healthbars;
    public Image fakehealthbars;

    PlayerCharacter Player;

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
    }

    void Update()
    {
        life();
    }
    public void life() 
    {
        healthbars.fillAmount = Player.Hp / Player.MaxHp;
        if (fakehealthbars.fillAmount > healthbars.fillAmount)
        {
            fakehealthbars.fillAmount -= 0.002f;
        }
        else
        {
            fakehealthbars.fillAmount = healthbars.fillAmount;
        }
    } 
}
