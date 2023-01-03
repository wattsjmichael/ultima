using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float moveSpeed;

    [HideInInspector]
    public Rigidbody2D rb;

    private Animator anim;
    public Animator wpnAnim;

    public SpriteRenderer sr;
    public Sprite[] playerDirectionSprites;

    public GameObject hitEffect;

    public float dashSpeed,
        dashLength,
        dashStamCost;
    private float dashCounter,
        activeMoveSpeed;

    private bool isKnockingBack;
    public float kbTime,
        kbForce;
    private float kbCounter;
    private Vector2 kbDir;

    public float totalStam,
        stamRefillSpeed;

    [HideInInspector]
    public float currStam;

    public bool isSpinning;
    public float spinCost,
        spinCooldown;
    private float spinCounter;

    public bool canMove;

    public SpriteRenderer swordSR;
    public Sprite[] allWpns;
    public DamageEnemy swordDMG;
    public int currentSword;

    private Vector3 respawnPos;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = SaveManager.instance.activeSave.sceneStartPos;
        currentSword = SaveManager.instance.activeSave.currentSword;
        swordSR.sprite = allWpns[currentSword];
        swordDMG.damToDeal = SaveManager.instance.activeSave.swordDamage;

        totalStam = SaveManager.instance.activeSave.maxStamina;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        activeMoveSpeed = moveSpeed;
        currStam = totalStam;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && !GameManager.instance.dialogActive)
        {
            if (!isKnockingBack)
            {
                rb.velocity =
                    new Vector2(
                        Input.GetAxisRaw("Horizontal"),
                        Input.GetAxisRaw("Vertical")
                    ).normalized * activeMoveSpeed;

                anim.SetFloat("Speed", rb.velocity.magnitude);

                if (rb.velocity != Vector2.zero)
                {
                    if (Input.GetAxisRaw("Horizontal") != 0)
                    {
                        sr.sprite = playerDirectionSprites[1];

                        if (Input.GetAxisRaw("Horizontal") < 0)
                        {
                            sr.flipX = true;
                            wpnAnim.SetFloat("dirX", -1f);
                            wpnAnim.SetFloat("dirY", 0f);
                        }
                        else
                        {
                            sr.flipX = false;
                            wpnAnim.SetFloat("dirX", 1f);
                            wpnAnim.SetFloat("dirY", 0f);
                        }
                    }
                    else
                    {
                        if (Input.GetAxisRaw("Vertical") < 0)
                        {
                            sr.sprite = playerDirectionSprites[0];
                            wpnAnim.SetFloat("dirX", 0f);
                            wpnAnim.SetFloat("dirY", -1f);
                        }
                        else
                        {
                            sr.sprite = playerDirectionSprites[2];
                            wpnAnim.SetFloat("dirX", 0f);
                            wpnAnim.SetFloat("dirY", 1f);
                        }
                    }
                }
                if (Input.GetMouseButtonDown(0) && !isSpinning)
                {
                    wpnAnim.SetTrigger("Attack");
                    AudioManager.instance.PlayerSFX(0);
                }
                if (dashCounter <= 0)
                {
                    if (Input.GetKeyDown(KeyCode.Space) && currStam >= dashStamCost)
                    {
                        activeMoveSpeed = dashSpeed;
                        dashCounter = dashLength;

                        currStam -= dashStamCost;
                    }
                }
                else
                {
                    dashCounter -= Time.deltaTime;
                    if (dashCounter <= 0)
                    {
                        activeMoveSpeed = moveSpeed;
                    }
                }

                if (spinCounter <= 0)
                {
                    if (Input.GetMouseButtonDown(1) && currStam >= spinCost)
                    {
                        wpnAnim.SetTrigger("SpinAttack");
                        currStam -= spinCost;

                        spinCounter = spinCooldown;
                        isSpinning = true;

                        AudioManager.instance.PlayerSFX(0);
                    }
                }
                else
                {
                    spinCounter -= Time.deltaTime;
                    if (spinCounter <= 0)
                    {
                        isSpinning = false;
                    }
                }

                currStam += stamRefillSpeed * Time.deltaTime;
                if (currStam > totalStam)
                {
                    currStam = totalStam;
                }
            }
            else
            {
                kbCounter -= Time.deltaTime;
                rb.velocity = kbDir * kbForce;
                if (kbCounter <= 0)
                {
                    isKnockingBack = false;
                }
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            anim.SetFloat("Speed", 0f);
        }
    }

    public void KnockBack(Vector3 kbPos)
    {
        kbCounter = kbTime;
        isKnockingBack = true;

        kbDir = transform.position - kbPos;
        kbDir.Normalize();

        Instantiate(hitEffect, transform.position, transform.rotation);
    }

    public void DoAtLevelStart()
    {
        canMove = true;
        respawnPos = transform.position;
    }

    public void UpgradeSword(int newDmg, int newSwordRef)
    {
        swordDMG.damToDeal = newDmg;
        currentSword = newSwordRef;
        swordSR.sprite = allWpns[newSwordRef];


        SaveManager.instance.activeSave.currentSword = currentSword;
        SaveManager.instance.activeSave.swordDamage = newDmg;
    }

    public void ResetOnRespawn()
    {
        transform.position = respawnPos;
        canMove = false;
        gameObject.SetActive(true);
        currStam = totalStam;
        kbCounter = 0;
        PlayerHealthController.instance.currHealth = PlayerHealthController.instance.maxHealth;
    }
}
