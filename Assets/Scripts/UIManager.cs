using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Slider healthSlider;
    public TMP_Text healthText;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealth()
    {
        healthSlider.maxValue = PlayerHealthController.instance.maxHealth;
        healthSlider.value = PlayerHealthController.instance.currHealth;
        healthText.text = "HEALTH " +  PlayerHealthController.instance.currHealth + "/" + PlayerHealthController.instance.maxHealth;

    }
}
