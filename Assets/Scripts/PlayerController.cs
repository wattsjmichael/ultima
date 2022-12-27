using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float moveSpeed;

    public Rigidbody2D rb;

    private Animator anim;
    public Animator weaponAnim;

    public SpriteRenderer sr;
    public Sprite[] playerDirectionSprites;

    public Joystick joystick;

    private bool isKnockingBack;
    public float kbTime,
        kbForce;
    private float kbCounter;
    private Vector2 kbDir;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(transform.position.x + (joystick.Horizontal) * moveSpeed * Time.deltaTime, transform.position.y + (joystick.Vertical) * moveSpeed * Time.deltaTime, transform.position.z);

        if (!isKnockingBack)
        {
            rb.velocity =
                new Vector2(joystick.Horizontal, joystick.Vertical).normalized * moveSpeed;

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
        } else {
            kbCounter -= Time.deltaTime;
            rb.velocity = kbDir * kbForce;
            if (kbCounter <= 0)
            {
                isKnockingBack = false;
            }
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

    }
}
