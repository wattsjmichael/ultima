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

     public Slider stamSlider;
    public TMP_Text stamText;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateHealth();
        UpdateStamina();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStamina();
    }

    public void UpdateHealth()
    {
        healthSlider.maxValue = PlayerHealthController.instance.maxHealth;
        healthSlider.value = PlayerHealthController.instance.currHealth;
        healthText.text = "HEALTH " +  PlayerHealthController.instance.currHealth + "/" + PlayerHealthController.instance.maxHealth;

    }

        public void UpdateStamina()
    {
        stamSlider.maxValue = PlayerController.instance.totalStam;
        stamSlider.value = PlayerController.instance.currStam;
        stamText.text = "STAMINA " +  Mathf.RoundToInt(PlayerController.instance.currStam) + "/" + PlayerController.instance.totalStam;

    }
}
