using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float moveSpeed;

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
    public float currStam;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        activeMoveSpeed = moveSpeed;
        currStam = totalStam;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!isKnockingBack)
        {
            rb.velocity =
                new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized
                * activeMoveSpeed;

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
            if (Input.GetMouseButtonDown(0))
            {
                wpnAnim.SetTrigger("Attack");
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

    public void KnockBack(Vector3 kbPos)
    {
        kbCounter = kbTime;
        isKnockingBack = true;

        kbDir = transform.position - kbPos;
        kbDir.Normalize();

        Instantiate(hitEffect, transform.position, transform.rotation);
    }
}
