using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{

    public int currHealth;
    public GameObject deathEffect;

    private EnemyController eEC;

    // Start is called before the first frame update
    void Start()
    {
        eEC = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDam(int damAmount)
    {
        currHealth -= damAmount;
        if (currHealth <= 0)
        {
            if(deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, transform.rotation);

            }
            Destroy(gameObject);
        }
        eEC.KnockBack(PlayerController.instance.transform.position);
    }
}
