//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Cinemachine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;

//public class PlayerCharacter : MonoBehaviour
//{
//    Rigidbody2D playerRig;
//    Animator playerAni;
//    Transform playerTra;
//    CapsuleCollider2D playerCol;
//    LayerMask ground;
//    GameObject JumpBox;
//    SpriteRenderer playerRenderer;
    
//    public GameObject ghosting;
//    public AudioSource music;
//    public List<AudioClip> list;

//    public float MaxHp;
//    public float Hp;

//    public int AttackMode;
//    public float LightAttackDamge;
//    public float HeavyAttackDamge;

//    public float Speed;
//    public float SlidSpeed;
//    public float JumpSpeed;
//    public float AttackSpeed;
//    public float HeavySpeed;
//    public float BeHitSpeed;
//    public float HeavyBeHitSpeed;

//    public float FallMultiplier;
//    public float LowJumpMultiplier;

//    public float BeHitShield; 

//    public Vector3 NowDir;

//    public bool IsGround;
//    public bool IsJump;
//    public bool IsSliding;
//    public bool IsAttack;
//    public bool canInput;
//    public bool canSliding;
//    public bool IsHit;
//    public bool IsDefend;
//    public bool IsDisplay;
//    public bool IsBack;
//    public bool IsMove;

//    public int JumpMax;
//    public int JumpCount;
//    public int ComboCount;

//    public float StartSlidTime;
//    public float SlidTime;
//    public float SlidCountTime;
//    public float SlidCd;
//    public float StartCombo;
//    public float ComboTime;
//    public float DefendTime;
//    public float FlashingTime;
//    public float FlashTep;
//    public float BackTime;

//    public Vector2 InitialSize;
//    public Vector2 InitialOffset;

//    public float time;
//    void Awake()
//    {
//        playerRig = GetComponent<Rigidbody2D>();
//        playerAni = GetComponent<Animator>();
//        playerTra = GetComponent<Transform>();
//        playerCol = GetComponent<CapsuleCollider2D>();
//        playerRenderer = GetComponent<SpriteRenderer>();
//        music = GetComponent<AudioSource>();
//        JumpBox = GameObject.Find("GroundCheck");
//        ground = 1 << 6;

//        MaxHp = 100;
//        Hp = MaxHp;

//        AttackMode = 0;
//        LightAttackDamge = 15f;
//        HeavyAttackDamge = 20f;

//        Speed = 5;
//        JumpSpeed = 6.5f;
//        SlidSpeed = 10;
//        AttackSpeed = 0.5f;
//        HeavySpeed = 0.2f;
//        BeHitSpeed = 0.5f;
//        HeavyBeHitSpeed = 10f;

//        FallMultiplier = 4f;
//        LowJumpMultiplier = 2.5f;

//        BeHitShield = 1f;

//        NowDir = playerTra.localScale;

//        IsJump = false;
//        IsSliding = false;
//        IsAttack = false;
//        canInput = true;
//        IsDisplay = true;
//        IsMove = true;

//        JumpMax = 2;
//        JumpCount = 0;
//        ComboCount = 1;

//        SlidTime = 0.5f;
//        SlidCd = 1f;
//        SlidCountTime = SlidCd;
//        ComboTime = 1f;
//        DefendTime = 3f;
//        FlashingTime = 0.2f;
//        FlashTep = 0;
//        BackTime = 0.2f;

//        InitialSize = new Vector2(playerCol.size.x, playerCol.size.y);
//        InitialOffset = new Vector2(playerCol.offset.x, playerCol.offset.y);
//    }
//    void Start()
//    {
        
//    }
//    void Update()
//    {
//        CheckGround();
//        PlayFallAni();
//        Defend();
//        Death();
//        SlidCountTime -= Time.deltaTime;
//    }
//    public void Move(float h)
//    {
//        if (!canInput || IsHit) 
//        {
//            return;
//        }
//        playerRig.velocity = new Vector2(h * Speed, playerRig.velocity.y);
//        if (h > 0) 
//        {
//            playerTra.localScale = new Vector2(-NowDir.x, NowDir.y);
//            if (!IsJump && !IsSliding && !IsAttack)
//            {
//                playerAni.Play("Run");
//                music.clip = list[4];
//                if (!music.isPlaying) 
//                {
//                    music.Play();
//                }
//            }
//        }
//        if (h < 0) 
//        {
//            playerTra.localScale = new Vector2(NowDir.x, NowDir.y);
//            if (!IsJump && !IsSliding && !IsAttack)
//            {
//                playerAni.Play("Run");
//                music.clip = list[4];
//                if (!music.isPlaying) 
//                {
//                    music.Play();
//                }
//            }
//        }
//        if (h == 0)
//        {
//            if (!IsJump && !IsSliding && !IsAttack && !playerAni.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
//            {
//                playerAni.Play("Idle");
//                music.Pause();
//            }
//        }
//    }
//    public void Jump() 
//    {
//        if (!canInput ) 
//        {
//            return;
//        }
//        if (IsGround)
//        {
//            IsJump = true;
//            playerAni.Play("Jump");
//            JumpCount = 0;
//            playerRig.velocity = new Vector2(playerRig.velocity.x, JumpSpeed);
//            JumpCount++;
//            IsGround = false;
//        }
//        else if (JumpCount > 0 && JumpCount < JumpMax) 
//        {
//            if (!IsHit) 
//            {
//                playerRig.velocity = new Vector2(playerRig.velocity.x, JumpSpeed);
//                JumpCount++;
//                playerAni.Play("Jump");
//            }
//        }
//    }
//    public void CheckGround()
//    {
//        IsGround = Physics2D.OverlapCircle(JumpBox.transform.position, 0.1f, ground);
//        IsJump = !IsGround;
//    }
//    public void PlayFallAni() 
//    {
//        if (!IsGround && canInput && !IsSliding && !IsHit) 
//        {
//            if (playerRig.velocity.y < 0)
//            {
//                playerRig.velocity += Vector2.up * Physics2D.gravity.y * (FallMultiplier - 1) * Time.deltaTime;
//                playerAni.Play("Fall");
//            }
//            else if (playerRig.velocity.y > 0 && Input.GetAxis("Jump")!=1) 
//            {
//                playerRig.velocity += Vector2.up * Physics2D.gravity.y * (LowJumpMultiplier - 1) * Time.deltaTime;
//            }
//        }
//    }
//    public void StartSliding() 
//    {
//        if (!IsSliding && canInput && IsGround && !IsHit && SlidCountTime <= 0)
//        {
//            IsSliding = true;
//            StartSlidTime = SlidTime;
//            playerCol.offset = new Vector2(playerCol.offset.x, -0.7f);
//            playerCol.size = new Vector2(playerCol.size.x, playerCol.size.x);
//            StartCoroutine(SlidMove(SlidTime));
//            playerAni.Play("Sliding");
//            gameObject.layer = LayerMask.NameToLayer("Flashing");
//        }
//    }
//    IEnumerator SlidMove(float time) 
//    {
//        canInput = false;
//        music.Pause();
//        if (NowDir.x == playerTra.localScale.x)
//        {
//            playerRig.velocity = new Vector2(-SlidSpeed, 0);
//        }
//        else if(NowDir.x == -playerTra.localScale.x)
//        {
//            playerRig.velocity = new Vector2(SlidSpeed, 0);
//        }
//        yield return new WaitForSeconds(time);
//        playerCol.offset = InitialOffset;
//        playerCol.size = InitialSize;
//        canInput = true;
//        IsSliding = false;
//        gameObject.layer = LayerMask.NameToLayer("Player");
//        SlidCountTime=SlidCd;
//    }
//    public void StartBackStep() 
//    {
//        if (!IsBack && IsGround && !IsHit)
//        {
//            IsBack = true;
//            StartCoroutine(Backstep(BackTime));
//            gameObject.layer = LayerMask.NameToLayer("Flashing");
//            ghosting.SetActive(true);
//        }
//    }
//    IEnumerator Backstep(float time) 
//    {
//        canInput = false;
//        music.Pause();
//        if (playerTra.localScale.x > 0)
//        {
//            playerRig.velocity = new Vector2(Speed, 0);
//        }
//        else if (playerTra.localScale.x < 0) 
//        {
//            playerRig.velocity = new Vector2(-Speed, 0);
//        }
//        yield return new WaitForSeconds(time);
//        canInput = true;
//        IsBack = false;
//        IsAttack = false;
//        ghosting.SetActive(false);
//        gameObject.layer = LayerMask.NameToLayer("Player");

//    }
//    public void Attack()
//    {
//        if (!IsAttack && !IsSliding && !IsHit)
//        {
//            IsAttack = true;
//            canInput = false;
//            AttackMode = 1;
//            playerRig.velocity = Vector2.zero;
//            ComboCount++;
//            if (ComboCount > 3)
//            {
//                ComboCount = 1;
//            }
//            StartCombo = ComboTime;
//            if (ComboCount == 1) 
//            {
//                playerAni.Play("Attack1");
//            }
//            if (ComboCount == 2)
//            {
//                playerAni.Play("Attack2");
//            }
//            if (ComboCount == 3)
//            {
//                playerAni.Play("Attack3");
//            }
//            playerRig.velocity = new Vector2(-playerTra.localScale.x * AttackSpeed, playerRig.velocity.y); 
//        }
//    }
//    public void HeavyAttack() 
//    {
//        if (!IsAttack && !IsSliding && !IsHit)
//        {
//            IsAttack = true;
//            canInput = false;
//            AttackMode = 2;
//            playerRig.velocity = Vector2.zero;
//            ComboCount++;
//            if (ComboCount > 3)
//            {
//                ComboCount = 1;
//            }
//            StartCombo = ComboTime;
//            if (ComboCount == 1)
//            {
//                playerAni.Play("Heavy Attack1");
//            }
//            if (ComboCount == 2)
//            {
//                playerAni.Play("Heavy Attack2");
//            }
//            if (ComboCount == 3)
//            {
//                playerAni.Play("Heavy Attack3");
//            }
//            playerRig.velocity = new Vector2(-playerTra.localScale.x * HeavySpeed, playerRig.velocity.y);
//        }
//    }
//    public void InAttack()
//    {
//        if (StartCombo != 0)
//        {
//            StartCombo -= Time.deltaTime;
//            if (StartCombo <= 0)
//            {
//                StartCombo = 0;
//                ComboCount = 0;
//            }
//        }
//    }
//    public void AttackPlayAudio() 
//    {
//        music.clip = list[0];
//        music.Play();
//    }
//    public void HeavyAttackPlayAudio() 
//    {
//        music.clip = list[1];
//        music.Play();
//    }
//    public void AttackOver() 
//    {
//        IsAttack = false;
//    }
//    public void CanInput() 
//    {
//        canInput = true;
//    }
//    public void BeHit(Vector2 Dir,float damge)
//    {
//        music.clip = list[3];
//        music.Play();
//        IsHit = true;
//        playerRig.velocity = Dir * BeHitSpeed;
//        playerAni.SetTrigger("Hit");
//        damge = damge * BeHitShield;
//        Hp -= damge;
//    }
//    public void Defend() 
//    {
//        if (IsDefend) 
//        {
//            DefendTime -= Time.deltaTime;
//            if (DefendTime > 0) 
//            {
//                gameObject.layer = LayerMask.NameToLayer("Flashing");
//                FlashTep += Time.deltaTime;
//                if (FlashTep >= FlashingTime)
//                {
//                    if (IsDisplay)
//                    {
//                        playerRenderer.enabled = false;
//                        IsDisplay = false;
//                        FlashTep = 0;
//                    }
//                    else 
//                    {
//                        playerRenderer.enabled = true;
//                        IsDisplay = true;
//                        FlashTep = 0;
//                    }
//                }
//            }
//            else if (DefendTime <= 0) 
//            {
//                gameObject.layer = LayerMask.NameToLayer("Player");
//                IsDefend = false;
//                IsDisplay = true;
//                playerRenderer.enabled = true;
//                DefendTime = 3f;
//            }
//        }
//    }
//    public void BeHitOver() 
//    {
//        IsHit = false;
//        IsDefend = true;
//    }
//    public void Death() 
//    {
//        if (Hp <= 0) 
//        {
            
//        }
//    }
//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.gameObject.CompareTag("Boss")) 
//        {
//            if (AttackMode == 1)
//            {
//                collision.GetComponent<FireCentipede>().BeHit(LightAttackDamge);
//            }
//            else if (AttackMode == 2) 
//            {
//                collision.GetComponent<FireCentipede>().BeHit(HeavyAttackDamge);
//            }
//        }
//    }
//}
