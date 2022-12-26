using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaActivator : MonoBehaviour
{

    private BoxCollider2D areaBox;
    // Start is called before the first frame update
    void Start()
    {
        areaBox = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CameraController.instance.areaBox = areaBox;
        }
    }
}
