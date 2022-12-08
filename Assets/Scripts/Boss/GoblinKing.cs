using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinKing : MonoBehaviour
{
    public enum BossState
    {
        Swing,
        Slash,
        Dash,
        Idle,
        BeHit,
        Death,
    }

    BossState state;

    public float Hp;
    private float MaxHp;
    private float IdleTime;
    private float SwingAttackTime; //swing times
    private float SlashAttackTime; //slash times
    private bool isHit;
    private bool isDead;
    private float ThrowDaggerAttackCd;

    void Awake()
    {
        state = BossState.Idle;
    }

    void Update()
    {
        switch (state)
        {
            case BossState.Dash:
                {
                    DashSkill();
                    break;
                }
            case BossState.Idle:
                {
                    IdleProccess();
                    break;
                }
            case BossState.BeHit:
                {
                    BeHitProccess();
                    break;
                }
            case BossState.Death:
                {
                    DeathProccess();
                    break;
                }
        }    
    }

    private void DashSkill()
    {
        throw new NotImplementedException();
    }

    private void IdleProccess()
    { 
        if (Hp <= MaxHp / 2 && IdleTime > 0)
        {
            SwingAttackTime = 5;
            SlashAttackTime = 7;
            IdleTime -= Time.deltaTime;
        }
        else if (Hp > MaxHp / 2 && IdleTime > 0)
        {
            SwingAttackTime = 3;
            SlashAttackTime = 5;
            IdleTime -= Time.deltaTime;
        }
        if (IdleTime <= 0 && !isHit && !isDead)
        {
            if (ThrowDaggerAttackCd <= 0 && Hp <= MaxHp / 2)
            {
                state = BossState.Slash;
            }
            else
            {
                state = BossState.Dash;
            }
        }
        else if (isHit && !isDead)
        {
            state = BossState.BeHit;
        }
        else if (isDead)
        {
            state = BossState.Death;
        }
    }

    private void BeHitProccess()
    {
        //C_ani.Play("BeHit");
        IdleTime -= Time.deltaTime;
        if (!isHit && !isDead)
        {
            state = BossState.Idle;
        }
        else if (isDead)
        {
            state = BossState.Death;
        }
    }
    public void BeHit(float Damge)
    {
        Hp -= Damge;
        isHit = true;
        //HitAudio.Play();
        //EffectAni.Play("1");
    }

    private void DeathProccess()
    {
        throw new NotImplementedException();
    }
}
