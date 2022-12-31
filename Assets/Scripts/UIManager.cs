using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject pauseScreen;

    public Slider healthSlider;
    public TMP_Text healthText;

    public Slider stamSlider;
    public TMP_Text stamText;

    public TMP_Text coinText;

    public string mainMenuScene;

    public GameObject blackoutScreen;
    

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealth();
        UpdateStamina();
        UpdateCoins();
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
        healthText.text =
            "HEALTH "
            + PlayerHealthController.instance.currHealth
            + "/"
            + PlayerHealthController.instance.maxHealth;
    }

    public void UpdateStamina()
    {
        stamSlider.maxValue = PlayerController.instance.totalStam;
        stamSlider.value = PlayerController.instance.currStam;
        stamText.text =
            "STAMINA "
            + Mathf.RoundToInt(PlayerController.instance.currStam)
            + "/"
            + PlayerController.instance.totalStam;
    }

    public void UpdateCoins()
    {
        if (coinText.text != null)
        {
            coinText.text = "Coins: " + GameManager.instance.currCoins.ToString();
        }
        else
        {
            Debug.Log("NULL IS UPDATE COINS");
        }
    }

    public void Resume()
    {
        GameManager.instance.PauseUnpause();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);

        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
