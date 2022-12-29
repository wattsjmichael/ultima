using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthToRestore;

    public float lifetime;
    // Start is called before the first frame update
    void Start()
    {
        if (lifetime > 0)
        {
        Destroy(gameObject, lifetime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private  void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerHealthController.instance.RestoreHealth(healthToRestore);
            Destroy(gameObject);
        }
        
    }
}
