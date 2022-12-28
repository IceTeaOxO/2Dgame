using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthbars : MonoBehaviour
{
    public Image healthbars;
    public Image fakehealthbars;

    public GoblinKing goblinKing;
    void Start()
    {
        
    }

    void Update()
    {
        if (goblinKing != null)
        {
            healthbars.fillAmount = goblinKing.Hp / goblinKing.MaxHp;
            if (fakehealthbars.fillAmount > healthbars.fillAmount)
            {
                fakehealthbars.fillAmount -= 0.001f;
            }
            else
            {
                fakehealthbars.fillAmount = healthbars.fillAmount;
            }
        }
    }
}
