using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerMobile : MonoBehaviour
{
    public static PlayerControllerMobile instance;
    public float moveSpeed;

    public Rigidbody2D rb;

    private Animator anim;
    public Animator weaponAnim;

    public SpriteRenderer sr;
    public Sprite[] playerDirectionSprites;

    public Joystick joystick;
    public GameObject hitEffect;

    public Button dash;

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
    private float currStam;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        dash = GetComponent<Button>();
        activeMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(transform.position.x + (joystick.Horizontal) * moveSpeed * Time.deltaTime, transform.position.y + (joystick.Vertical) * moveSpeed * Time.deltaTime, transform.position.z);

        if (!isKnockingBack)
        {
            rb.velocity =
                new Vector2(joystick.Horizontal, joystick.Vertical).normalized * activeMoveSpeed;

            anim.SetFloat("Speed", rb.velocity.magnitude);

            if (rb.velocity != Vector2.zero)
            {
                if (joystick.Horizontal != 0)
                {
                    sr.sprite = playerDirectionSprites[1];

                    if (joystick.Horizontal < 0)
                    {
                        sr.flipX = true;
                        weaponAnim.SetFloat("dirX", -1f);
                        weaponAnim.SetFloat("dirY", 0f);
                    }
                    else
                    {
                        sr.flipX = false;
                        weaponAnim.SetFloat("dirX", 1f);
                        weaponAnim.SetFloat("dirY", 0f);
                    }
                }
                else
                {
                    if (joystick.Vertical < 0)
                    {
                        sr.sprite = playerDirectionSprites[0];
                        weaponAnim.SetFloat("dirX", 0f);
                        weaponAnim.SetFloat("dirY", -1f);
                    }
                    else
                    {
                        sr.sprite = playerDirectionSprites[2];
                        weaponAnim.SetFloat("dirX", 0f);
                        weaponAnim.SetFloat("dirY", 1f);
                    }
                }
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
        Debug.Log(currStam);
        currStam += stamRefillSpeed * Time.deltaTime;
        if (currStam > totalStam)
        {
            currStam = totalStam;
        }
    }

    public void dashButton()
    {
        if (currStam >= dashStamCost)
        {
            if (dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;

                currStam -= dashStamCost;
            }
            else
            {
                dashCounter -= Time.deltaTime;

                if (dashCounter <= 0)
                {
                    activeMoveSpeed = moveSpeed;
                }
            }
        }
        else
        {
            Debug.Log("Outta Stam!");
           
        }
    }

    public void attackButton()
    {
        weaponAnim.SetTrigger("Attack");
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
