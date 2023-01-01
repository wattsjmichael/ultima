using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShotController : MonoBehaviour
{
    public float bulletSpeed,
        rotSpeed;
    public int damToPlayer;
    private Vector3 moveDir;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(
            transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y,
            transform.rotation.eulerAngles.z + (rotSpeed * Time.deltaTime)
        );
        transform.position += moveDir * bulletSpeed * Time.deltaTime;
    }

    public void SetDirection(Vector3 spawnPos)
    {
        moveDir = transform.position - spawnPos;
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
