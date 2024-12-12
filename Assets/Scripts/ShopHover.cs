using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopHover : MonoBehaviour
{
    public GameObject shopGUI;

    private GameObject playerObject;
    private PlayerAttackArea playerAttackArea;
    private PlayerMovement playerMovement;
    private ShopUIController shopUIController;


    public int speedCost;
    public int attackCost;

    public GameObject moneyUI;
    private MoneyController moneyController;

    private bool playerInside;

    private void Awake()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerAttackArea = playerObject.GetComponentInChildren<PlayerAttackArea>();
        playerMovement = playerObject.GetComponent<PlayerMovement>();
        moneyController = moneyUI.GetComponent<MoneyController>();
        shopUIController = shopGUI.GetComponent<ShopUIController>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerInside = true;
            shopGUI.SetActive(true);
        }
    }

    public void UpgradeSpeed()
    {
        if(!playerInside)
        {
            return;
        }
        if (moneyController.totalMoney >= speedCost)
        {
            moneyController.DecreaseMoney(speedCost);
            playerMovement.speed += 1;
            int priceCost = 1500;
            shopUIController.UpdateSpeedPriceCost(priceCost);
            speedCost += 1500;

            
            
        }
    }

    public void UpgradeAttack()
    {
        if (!playerInside)
        {
            return;
        }
        if (moneyController.totalMoney >= attackCost)
        {
            moneyController.DecreaseMoney(attackCost);
            playerAttackArea.playerDamage += 1;
            int priceCost = 2500;
            shopUIController.UpdateAttackPriceCost(priceCost);
            attackCost += 2500;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerInside = false;
            shopGUI.SetActive(false);
        }
    }
}
