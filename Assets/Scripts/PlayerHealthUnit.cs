using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerHealthUnit : MonoBehaviour
{

    public float MaxHp;
    public float currentHp;
    private Animator animator;

    public GameObject gameOverUI;
    public GameObject repairTowerArea;

    public bool isCastle;
    public GameObject towerHeathBarUI;
    private TowerHealthBar healthBar;

    private void Awake()
    {
        healthBar = GetComponentInChildren<TowerHealthBar>();
    }

    private void Start()
    {
        currentHp = MaxHp;
        animator = GetComponent<Animator>();
    }

    public void GetDamage(float damage)
    {
        this.currentHp -= damage;
        healthBar.UpdateHealthBar(currentHp, MaxHp);
        animator.SetTrigger("Hit");
        if (this.currentHp <= 0 )
        {
            Die();
        }
    }

    public void Die()
    {
        if(isCastle)
        {
            towerHeathBarUI.SetActive(false);
            gameOverUI.SetActive(true);
            Time.timeScale = 0;
            animator.SetBool("Destroyed", true);
            gameObject.tag = "DestroyedTower";
        }
        else
        {
            towerHeathBarUI.SetActive(false);
            repairTowerArea.SetActive(true);
            animator.SetBool("Destroyed", true);
            gameObject.tag = "DestroyedTower";
        }

    }

    public void repair()
    {
        towerHeathBarUI.SetActive(true);
        animator.SetBool("Destroyed", false);
        gameObject.tag = "Tower";
        currentHp = MaxHp;
    }

}
