using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairController : MonoBehaviour
{
    public GameObject RepairUI;
    public GameObject moneyUI;

    public GameObject player;
    public Rigidbody2D rb;
    public MoneyController moneyController;
    public PlayerHealthUnit towerUnit;

    public int repairCost;

    public bool playerEntered;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        rb = player.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            moneyController = moneyUI.GetComponent<MoneyController>();
            RepairUI.SetActive(true);
            if(moneyController != null)
            {
                playerEntered = true;
            }

        }
    }

    private void Update()
    {
        if(playerEntered)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (moneyController.totalMoney >= repairCost)
                {
                    moneyController.DecreaseMoney(repairCost);
                    towerUnit.repair();
                }
                else
                {
                    Debug.Log("UANG : " + moneyController.totalMoney);
                }


            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        RepairUI.SetActive(false);
    }
}
