using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    public Rigidbody2D rb;

    private Animator anim;

    public SpriteRenderer sr;
    public Sprite[] playerDirectionSprites;

    public Joystick joystick;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(transform.position.x + (joystick.Horizontal) * moveSpeed * Time.deltaTime, transform.position.y + (joystick.Vertical) * moveSpeed * Time.deltaTime, transform.position.z);




        rb.velocity = new Vector2(joystick.Horizontal, joystick.Vertical).normalized * moveSpeed;

        anim.SetFloat("Speed", rb.velocity.magnitude);

        if (rb.velocity != Vector2.zero)
        {
            if (joystick.Horizontal != 0)
            {
                sr.sprite = playerDirectionSprites[1];

                if (joystick.Horizontal < 0)
                {
                    sr.flipX = true;
                }
                else
                {
                    sr.flipX = false;
                }
            }
            else
            {
                if (joystick.Vertical < 0)
                {
                    sr.sprite = playerDirectionSprites[0];
                }
                else
                {
                    sr.sprite = playerDirectionSprites[2];
                }
            }
        }
    }
}
