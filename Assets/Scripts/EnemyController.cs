using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D eRB;
    public Animator eAnim;
    public BoxCollider2D area;

    public float moveSpeed;

    public float waitTime,
        moveTime;
    private float waitCounter,
        moveCounter;

    private Vector2 moveDir;

    public bool shouldChase;
    private bool isChasing;
    public float chaseSpeed,
        rangeToChase,
        waitAfterHitting;

    public int damToDeal = 10;

    private bool isKnockingBack;
    public float kbTime,
        kbForce,
        waitAfterKB;
    private float kbCounter,
        kbWaitCounter;
    private Vector2 kbDir;

    // Start is called before the first frame update
    void Start()
    {
        waitCounter = Random.Range(waitTime * .75f, waitTime * 1.25f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isKnockingBack)
        {
            if (!isChasing)
            {
                if (waitCounter > 0)
                {
                    waitCounter = waitCounter - Time.deltaTime;
                    eRB.velocity = Vector2.zero;

                    if (waitCounter <= 0)
                    {
                        moveCounter = moveCounter = Random.Range(moveTime * .75f, moveTime * 1.25f);
                        eAnim.SetBool("moving", true);
                        moveDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                        moveDir.Normalize();
                    }
                }
                else
                {
                    moveCounter -= Time.deltaTime;

                    eRB.velocity = moveDir * moveSpeed;

                    if (moveCounter <= 0)
                    {
                        waitCounter = Random.Range(waitTime * .75f, waitTime * 1.25f);
                        eAnim.SetBool("moving", false);
                    }
                    if (shouldChase && PlayerController.instance.gameObject.activeInHierarchy)
                    {
                        if (
                            Vector3.Distance(
                                transform.position,
                                PlayerController.instance.transform.position
                            ) < rangeToChase
                        )
                        {
                            isChasing = true;
                        }
                    }
                }
            }
            else
            {
                if (waitCounter > 0)
                {
                    waitCounter -= Time.deltaTime;
                    eRB.velocity = Vector2.zero;

                    if (waitCounter <= 0)
                    {
                        eAnim.SetBool("moving", true);
                    }
                }
                else
                {
                    moveDir = PlayerController.instance.transform.position - transform.position;
                    moveDir.Normalize();

                    eRB.velocity = moveDir * chaseSpeed;
                }

                if (
                    Vector3.Distance(
                        transform.position,
                        PlayerController.instance.transform.position
                    ) > rangeToChase
                    || !PlayerController.instance.gameObject.activeInHierarchy
                )
                {
                    isChasing = false;
                    waitCounter = waitTime;
                    eAnim.SetBool("moving", false);
                }
            }
        }
        else
        {
            if (kbCounter > 0)
            {
                kbCounter -= Time.deltaTime;

                eRB.velocity = kbDir * kbForce;
                if (kbCounter <= 0)
                {
                    kbWaitCounter = waitAfterKB;
                }
            }
            else
            {
                kbWaitCounter -= Time.deltaTime;
                eRB.velocity = Vector2.zero;

                if(kbCounter <= 0)
                {
                    isKnockingBack = false;
                }
            }
        }

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, area.bounds.min.x + 1f, area.bounds.max.x - 1f),
            Mathf.Clamp(transform.position.y, area.bounds.min.y + 1f, area.bounds.max.y - 1f),
            transform.position.z
        );
    }

    public void KnockBack(Vector3 kbPos)
    {
        kbCounter = kbTime;
        isKnockingBack = true;

        kbDir = transform.position - kbPos;
        kbDir.Normalize();

       

       
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (isChasing)
            {
                waitCounter = waitAfterHitting;
                eAnim.SetBool("moving", false);

                PlayerController.instance.KnockBack(transform.position);

                PlayerHealthController.instance.DmgPlayer(damToDeal);
            }
        }
    }
}
