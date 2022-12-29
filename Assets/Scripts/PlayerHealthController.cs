using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public int currHealth;
    public int maxHealth;

    public float invcLength = 1f;
    private float invcCounter;

    public GameObject deathEffect;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;

        UIManager.instance.UpdateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if (invcCounter > 0)
        {
            invcCounter -= Time.deltaTime;
        }
    }

    public void DmgPlayer(int damAmount)
    {
        if (invcCounter <= 0)
        {
            currHealth -= damAmount;
            invcCounter = invcLength;

            if (currHealth <= 0)
            {
                currHealth = 0;

                gameObject.SetActive(false);
                Instantiate(deathEffect, transform.position, transform.rotation);


            }
                UIManager.instance.UpdateHealth();
        }
    }

    public void RestoreHealth(int healthToRestore)
    {
        currHealth += healthToRestore;
        if (currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }
        UIManager.instance.UpdateHealth();
    }
}
