using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{

    private bool inRange;
    public GameObject objectToSwitchOff;
    private bool isOn;

    public SpriteRenderer switchSprite;
    public Sprite offSprite;
    public Sprite onSprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inRange)
        {
            if(Input.GetMouseButtonDown(0))
            {
                isOn = !isOn;
                
                if (isOn)
                {
                    switchSprite.sprite = onSprite;
                } 
                else
                {
                    switchSprite.sprite = offSprite;
                }
                objectToSwitchOff.SetActive(!isOn);

            }
        }
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag  == "Player")
        {
            inRange = true;
        }
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
         if(other.tag  == "Player")
        {
            inRange = false;
        }
    }
}
