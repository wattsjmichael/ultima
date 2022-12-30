using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{

    public int currHealth;
    public GameObject deathEffect;

    private EnemyController eEC;

    public GameObject healthToDrop, coinToDrop;
    public float healthDropChance, coinDropChance;

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

            if (Random.Range(0f, 100f) < healthDropChance && healthToDrop != null)
            {
                Instantiate(healthToDrop, transform.position, transform.rotation);
            }

            if (Random.Range(0f, 100f) < coinDropChance && coinToDrop != null)
            {
                Instantiate(coinToDrop, transform.position, transform.rotation);
            }
            AudioManager.instance.PlayerSFX(4);

        }
        AudioManager.instance.PlayerSFX(7);

        eEC.KnockBack(PlayerController.instance.transform.position);
    }
}
