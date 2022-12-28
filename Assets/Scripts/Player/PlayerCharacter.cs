using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour
{
    Rigidbody2D playerRig;
    Transform playerTra;
    Transform ThrowPoint;
    PolygonCollider2D playerCol;
    LayerMask ground;
    GameObject JumpBox;   
    SpriteRenderer playerRenderer;

    public GameManager gm;
    public GameObject Axe;
    public AudioSource music;
    public List<AudioClip> list;

    public float MaxHp;
    public float Hp;

    public int AttackMode;
    public float LightAttackDamge;

    public float Speed;
    public float JumpSpeed;
    public float AttackSpeed;
    public float BeHitSpeed;

    public float FallMultiplier;
    public float LowJumpMultiplier;

    public float BeHitShield;

    public Vector3 NowDir;

    public bool IsGround;
    public bool IsJump;
    public bool IsAttack;
    public bool canInput;
    public bool IsHit;
    public bool IsDefend;
    public bool IsDisplay;
    public bool IsMove;

    public int JumpMax;
    public int JumpCount;
    public int ComboCount;

    public float StartCombo;
    public float ComboTime;
    public float DefendTime;
    public float FlashingTime;
    public float FlashTep;

    public float time;
    void Awake()
    {
        playerRig = GetComponent<Rigidbody2D>();
        playerTra = GetComponent<Transform>();
        playerCol = GetComponent<PolygonCollider2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
        music = GetComponent<AudioSource>();
        JumpBox = GameObject.Find("GroundCheck");
        ThrowPoint = transform.Find("ThrowPoint");
        ground = 1 << 6;

        MaxHp = 250;
        Hp = MaxHp;

        AttackMode = 0;
        LightAttackDamge = 15f;

        Speed = 15;
        JumpSpeed = 20f;
        AttackSpeed = 0.5f;
        BeHitSpeed = 0.5f;

        FallMultiplier = 1f;
        LowJumpMultiplier = 15f;

        BeHitShield = 1f;

        NowDir = playerTra.localScale;

        IsJump = false;
        IsAttack = false;
        canInput = true;
        IsDisplay = true;
        IsMove = true;

        JumpMax = 5;
        JumpCount = 0;
        ComboCount = 1;

        ComboTime = 1f;
        DefendTime = 3.5f;
        FlashingTime = 0.2f;
        FlashTep = 0;
    }
    void Start()
    {

    }
    void Update()
    {
        CheckGround();
        PlayFallAni();
        Defend();
        Death();
    }
    public void Move(float h)
    {
        if (!canInput || IsHit)
        {
            return;
        }
        playerRig.velocity = new Vector2(h * Speed, playerRig.velocity.y);
        if (h > 0)
        {
            playerTra.localScale = new Vector2(NowDir.x, NowDir.y);
            if (!IsJump && !IsAttack)
            {
                music.clip = list[2];
                if (!music.isPlaying)
                {
                    music.Play();
                }
            }
        }
        if (h < 0)
        {
            playerTra.localScale = new Vector2(-NowDir.x, NowDir.y);
            if (!IsJump && !IsAttack)
            {
                music.clip = list[2];
                if (!music.isPlaying)
                {
                    music.Play();
                }
            }
        }
        if (h == 0)
        {
            if (!IsJump && !IsAttack)
            {
                music.Pause();
            }
        }
    }
    public void Jump()
    {
        if (!canInput)
        {
            return;
        }
        if (IsGround)
        {
            IsJump = true;
            JumpCount = 0;
            playerRig.velocity = new Vector2(playerRig.velocity.x, JumpSpeed);
            JumpCount++;
            IsGround = false;
        }
        else if (JumpCount > 0 && JumpCount < JumpMax)
        {
            if (!IsHit)
            {
                playerRig.velocity = new Vector2(playerRig.velocity.x, JumpSpeed);
                JumpCount++;            }
        }
    }
    public void CheckGround()
    {
        IsGround = Physics2D.OverlapCircle(JumpBox.transform.position, 0.1f, ground);
        IsJump = !IsGround;
    }
    public void PlayFallAni()
    {
        if (!IsGround && canInput && !IsHit)
        {
            if (playerRig.velocity.y < 0)
            {
                playerRig.velocity += Vector2.up * Physics2D.gravity.y * (FallMultiplier - 1) * Time.deltaTime;
            }
            else if (playerRig.velocity.y > 0 && Input.GetAxis("Jump") != 1)
            {
                playerRig.velocity += Vector2.up * Physics2D.gravity.y * (LowJumpMultiplier - 1) * Time.deltaTime;
            }
        }
    }
    public void Attack()
    {
        if (!IsAttack && !IsHit)
        {
            IsAttack = true;
            canInput = false;
            AttackMode = 1;
            playerRig.velocity = Vector2.zero;
            ComboCount++;
            if (ComboCount > 3)
            {
                ComboCount = 1;
            }
            StartCombo = ComboTime;
            if (ComboCount == 1)
            {
                AttackPlayAudio();
                AxeCreate();
                AttackOver();
                CanInput();
            }
            if (ComboCount == 2)
            {
                AttackPlayAudio();
                AxeCreate();
                AttackOver();
                CanInput();
            }
            if (ComboCount == 3)
            {
                AttackPlayAudio();
                AxeCreate();
                AttackOver();
                CanInput();
            }
            playerRig.velocity = new Vector2(-playerTra.localScale.x * AttackSpeed, playerRig.velocity.y);
        }
    }
    public void AxeCreate()
    {
        //GameObject axe = Instantiate(Axe, null);
        //axe.transform.position = ThrowPoint.position + NowDir * 1.0f;

        if (NowDir.x > 0)
        {
                GameObject axe = Instantiate(Axe, null);
                Vector3 dir = transform.right;
                axe.transform.position = ThrowPoint.position + dir * 1.0f;
        }
        else if (NowDir.x < 0)
        {
                GameObject axe = Instantiate(Axe, null);
                Vector3 dir = -transform.right;
                axe.transform.position = ThrowPoint.position + dir * 1.0f;
        }
    }
    public void InAttack()
    {
        if (StartCombo != 0)
        {
            StartCombo -= Time.deltaTime;
            if (StartCombo <= 0)
            {
                StartCombo = 0;
                ComboCount = 0;
            }
        }
    }
    public void AttackPlayAudio()
    {
        music.clip = list[0];
        music.Play();
    }
    public void AttackOver()
    {
        IsAttack = false;
    }
    public void CanInput()
    {
        canInput = true;
    }
    public void BeHit(Vector2 Dir, float damge)
    {
        music.clip = list[1];
        music.Play();
        IsHit = true;
        playerRig.velocity = Dir * BeHitSpeed;
        BeHitOver();
        AttackOver();
        CanInput();
        damge = damge * BeHitShield;
        Hp -= damge;
    }
    public void Defend()
    {
        if (IsDefend)
        {
            canInput = true;
            DefendTime -= Time.deltaTime;
            if (DefendTime > 0)
            {
                gameObject.tag = "Flashing";
                gameObject.layer = LayerMask.NameToLayer("Flashing");
                FlashTep += Time.deltaTime;
                if (FlashTep >= FlashingTime)
                {
                    if (IsDisplay)
                    {
                        playerRenderer.enabled = false;
                        IsDisplay = false;
                        FlashTep = 0;
                    }
                    else
                    {
                        playerRenderer.enabled = true;
                        IsDisplay = true;
                        FlashTep = 0;
                    }
                }
            }
            else if (DefendTime <= 0)
            {
                gameObject.tag = "Player";
                gameObject.layer = LayerMask.NameToLayer("Player");
                IsDefend = false;
                IsDisplay = true;
                playerRenderer.enabled = true;
                DefendTime = 5f;
            }
        }
    }
    public void BeHitOver()
    {
        IsHit = false;
        IsDefend = true;
    }
    public void Death()
    {
        if (Hp <= 0)
        {
            gm.GameOver();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            if (AttackMode == 1)
            {
                collision.GetComponent<GoblinKing>().BeHit(LightAttackDamge);
            }
        }
    }
}
