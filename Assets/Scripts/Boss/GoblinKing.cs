using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossState
{
    Swing,
    Slash,
    Dash,
    Idle,
    BeHit,
    Death,
}

public class GoblinKing : MonoBehaviour
{
    Transform C_tra;
    Transform Dagger;
    Rigidbody2D C_rig;
    Animator C_ani;
    BackGround background;//change background(light dim)
    public AudioSource HitAudio;
    public AudioSource AttackAudio;
    public Animator EffectAni;//play SFX
    public GameObject Tornado;
    public GameObject Player;

    Vector2 Initial;

    BossState state;

    public float MaxHp;
    public float Hp;

    public float MoveDamge;

    public float Speed;

    public float IdleTime;
    public float SlashAttackCd;
    public float time;

    public int SwingAttackTime;//swing times
    public int SlashAttackTime;//slash times

    public bool isHit;
    public bool isDead;

    void Awake()
    {
        C_tra = GetComponent<Transform>();
        C_rig = GetComponent<Rigidbody2D>();
        C_ani = GetComponent<Animator>();
        background = GameObject.Find("BlackCloth").GetComponent<BackGround>();
        Player = GameObject.Find("Player");
        Dagger = transform.Find("Dagger");

        Initial = C_tra.localScale;

        state = BossState.Idle;

        MaxHp = 200;
        Hp = MaxHp;

        MoveDamge = 20;

        Speed = 20;

        isDead = false;

        IdleTime = 3f;
        SlashAttackCd = 20f;
        time = 1f;

        SwingAttackTime = 3;
        SlashAttackTime = 7;
    }
        void Update()
    {
        CheckHp();
        switch (state) 
        {
            case BossState.Swing: 
                {
                    SwingAttack();
                    break;
                }
            case BossState.Slash: 
                {
                    SlashAttack();
                    
                    break;
                }
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
                    C_ani.Play("GK_death");
                    break;
                }
        }
    }
    public void SwingAttack() 
    {
        C_ani.Play("GK_attack1");
        SlashAttackCd -= Time.deltaTime;
        if (SwingAttackTime <= 0 && !isDead)
        {
            state = BossState.Idle;
            SwingAttackTime -= 1;
        }
        else if (isDead)
        {
            state = BossState.Death;
        }
    }
    public void TornadoCreate() 
    {
        if (C_tra.localScale.x == Initial.x)
        {
            for (int i = -5; i < 2; i++)
            {
                GameObject tornado = Instantiate(Tornado, null);
                Vector3 dir = Quaternion.Euler(0, i * 15, 0) * -transform.right;
                tornado.transform.position = Dagger.position + dir * 1.0f;
                tornado.transform.rotation = Quaternion.Euler(0, 0, i * 15);
            }
        }
        else if (C_tra.localScale.x == -Initial.x)
        {
            for (int i = -1; i < 5; i++)
            {
                GameObject tornado = Instantiate(Tornado, null);
                Vector3 dir = Quaternion.Euler(0, i * 15, 0) * transform.right;
                tornado.transform.position = Dagger.position + dir * 1.0f;
                tornado.transform.rotation = Quaternion.Euler(0, 0, i * 15);
            }
        }
        SwingAttackTime -= 1;
    }
    public void DashSkill() 
    {
        if (!isDead)
        {
            Dash();
            IdleTime = 5f;
        }
        else if (isDead)
        {
            state = BossState.Death;
        }
    }
    public void Dash()//run 
    {
        if (C_tra.localScale.x==Initial.x)
        {
            C_ani.Play("GK_run");
            C_rig.velocity = new Vector2(-Speed, C_rig.velocity.y);
        }
        else if (C_tra.localScale.x == -Initial.x) 
        {
            C_ani.Play("GK_run");
            C_rig.velocity = new Vector2(Speed, C_rig.velocity.y);
        }
    }
    public void SlashAttack() 
    {
        C_ani.Play("GK_attack2");
        background.isChange = true;
        SlashAttackCd = 20f;
        if (SlashAttackTime <= 0 && !isDead)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                state = BossState.Idle;
                time = 1;
            }
        }
        else if (isDead)
        {
            state = BossState.Death;
        }
    }
    public void CreateFirePillar() //editing
    {
        //for (int i = 0; i < 5; i++)
        //{
        //    int r = Random.Range(-14, 14);
        //    GameObject firepillar = Instantiate(FirePillar, null);
        //    firepillar.transform.position = new Vector3(r, 6, 0);
        //}
        SlashAttackTime -= 1;
    }
    public void IdleProccess() 
    {
        C_ani.Play("GK_idle");
        background.isBack = true;
        SlashAttackCd -= Time.deltaTime;
        IdleTime -= Time.deltaTime;
        if (Hp <= MaxHp / 2 && IdleTime > 0)
        {
            SwingAttackTime = 3;
            SlashAttackTime = 7;
        }
        else if (Hp > MaxHp / 2 && IdleTime > 0)
        {
            SwingAttackTime = 5;
            SlashAttackTime = 7;  
        }
        if (IdleTime <= 0 && !isHit && !isDead)
        {
            if (SlashAttackCd <= 0 && Hp <= MaxHp / 2)
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
    public void BeHitProccess()
    {
        C_ani.Play("GK_takeHit");
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
        HitAudio.Play();
        EffectAni.Play("1");
    }
    public void BeHitOver() 
    {
        isHit = false;
    }
    public void CheckHp() 
    {
        if (Hp <= 0) 
        {
            isDead = true;
        }
    }
    public void Death() 
    {
        Destroy(gameObject);
    }
    public void PlayAttackAudio() 
    {
        AttackAudio.Play();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("AirWall"))
        {
            C_tra.localScale = new Vector3(-C_tra.localScale.x, C_tra.localScale.y, C_tra.localScale.z);
            state = BossState.Swing;
        }
        else if (collision.collider.CompareTag("Player") && state==BossState.Dash) 
        {
            if (C_tra.position.x < Player.transform.position.x)
            {
                collision.collider.GetComponent<PlayerController>().BeHit(Vector2.right,MoveDamge);
            }
            else if (C_tra.position.x >= Player.transform.position.x) 
            {
                collision.collider.GetComponent<PlayerController>().BeHit(Vector2.left,MoveDamge);
            }
        }
    }
}
