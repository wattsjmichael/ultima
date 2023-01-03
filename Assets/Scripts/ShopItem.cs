using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [TextArea]
    public string description;
    public int itemCost;
    private bool itemActive;
    public bool isHealthUpgrade, isStamUpgrade;
    public int amountToAdd;

    public bool removeAfterPurchase;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (itemActive)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if (GameManager.instance.currCoins >= itemCost)
                {
                    GameManager.instance.currCoins -= itemCost;
                    UIManager.instance.UpdateCoins();

                    if (isHealthUpgrade)
                    {
                        PlayerHealthController.instance.maxHealth += amountToAdd;
                        PlayerHealthController.instance.currHealth += amountToAdd;

                        SaveManager.instance.activeSave.maxHealth = PlayerHealthController.instance.maxHealth;

                        UIManager.instance.UpdateHealth();

                    }

                     if (isStamUpgrade)
                    {
                        PlayerController.instance.totalStam += amountToAdd;
                        PlayerController.instance.currStam += amountToAdd;

                        SaveManager.instance.activeSave.maxStamina = PlayerController.instance.totalStam;

                        UIManager.instance.UpdateStamina();

                    }

                    SaveManager.instance.activeSave.currCoins = GameManager.instance.currCoins;
                        if(removeAfterPurchase)
                        {
                            gameObject.SetActive(false);

                        }
                        DialogManager.instance.dialogBox.SetActive(false);
                        itemActive = false;
                }
                else
                {
                    DialogManager.instance.dialogText.text = "Not Enough Gold!";
                }
            }
        }
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
       if(other.tag == "Player")
       {
        itemActive = true;

        DialogManager.instance.dialogBox.SetActive(true);
        DialogManager.instance.dialogText.text = description;


       } 
    }

        void OnTriggerExit2D(Collider2D other)
    {
       if(other.tag == "Player")
       {
        itemActive = false;

        DialogManager.instance.dialogBox.SetActive(false);
        DialogManager.instance.dialogText.text = description;
        

       } 
    }
}
