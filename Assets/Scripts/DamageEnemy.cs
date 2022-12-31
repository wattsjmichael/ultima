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

               SpawnHitEffect();
            }
            other.GetComponent<EnemyHealthController>().TakeDam(damToDeal);
            SpawnHitEffect();
        }

        if(other.tag == "Breakable")
        {
            other.GetComponent<BreakableObject>().Break();
            SpawnHitEffect();

        }

        if (other.tag == "Boss")
        {
            other.GetComponent<BossWeakpoint>().DamaageBoss(damToDeal);
            SpawnHitEffect();
        }
    }

    void SpawnHitEffect()
    {
         Instantiate(hitEffect, transform.position, transform.rotation);
    }
}
