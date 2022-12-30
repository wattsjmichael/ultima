using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    public int damToDeal;

    public GameObject hitEffect;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            if (PlayerController.instance.isSpinning == true)
            {
                other.GetComponent<EnemyHealthController>().TakeDam(damToDeal * 4);

                Instantiate(hitEffect, transform.position, transform.rotation);
            }
            other.GetComponent<EnemyHealthController>().TakeDam(damToDeal);

            Instantiate(hitEffect, transform.position, transform.rotation);
        }
    }
}
