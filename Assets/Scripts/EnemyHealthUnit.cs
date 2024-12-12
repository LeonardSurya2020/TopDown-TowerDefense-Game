using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthUnit : MonoBehaviour
{

    public float MaxHp;
    public float currentHp;
    private Animator animator;
    public int moneyPerKill = 50;

    private TowerHealthBar healthBar;

    public GameObject moneyPointText;
    private MoneyController moneyController;

    private void Awake()
    {
        //healthBar = GetComponentInChildren<TowerHealthBar>();
        moneyPointText = GameObject.FindGameObjectWithTag("MoneyUI");
    }

    private void Start()
    {
        currentHp = MaxHp;
        animator = GetComponent<Animator>();
    }

    public void GetDamage(float damage)
    {
        this.currentHp -= damage;
        //healthBar.UpdateHealthBar(currentHp, MaxHp);
        //animator.SetTrigger("Hit");
        if (this.currentHp <= 0)
        {
            moneyController = moneyPointText.GetComponent<MoneyController>();
            moneyController.AddMoney(moneyPerKill);
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
