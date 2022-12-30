using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int currCoins;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void GetCoins(int coinsToAdd)
    {
        currCoins += coinsToAdd;

        UIManager.instance.UpdateCoins();
    }

    public void PauseUnpause()
    {
        if (!UIManager.instance.pauseScreen.activeInHierarchy)
        {
            UIManager.instance.pauseScreen.SetActive(true);
            Time.timeScale = 0f;

            PlayerController.instance.canMove = false;
        }
        else
        {
            UIManager.instance.pauseScreen.SetActive(false);
            Time.timeScale = 1f;
            PlayerController.instance.canMove = true;
        }
    }
}
