using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    public float bulletSpeed;
    public int damToPlayer;
    private Vector3 moveDir;
    // Start is called before the first frame update
    void Start()
    {
        moveDir = PlayerController.instance.transform.position - transform.position;
        moveDir.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDir * bulletSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerHealthController.instance.DmgPlayer(damToPlayer);

        }

        Destroy(gameObject);
    }


    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
