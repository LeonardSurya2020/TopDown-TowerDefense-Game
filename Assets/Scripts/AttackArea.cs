using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{

    public float enemyDamage = 3;
    //private Health health;
    private PlayerMovement playerMovement;
    public EnemyAI enemyAI;
    public GameObject enemyObject;
    public float throwForce = 10f;
    private HitFlash playerHitFlash;
    private HitFlash towerHitFlash;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(enemyAI.isNotGoblinPatrol == true)
        {
            if (collision.gameObject.CompareTag("Tower") || collision.gameObject.CompareTag("Castle"))
            {

                Debug.Log("kenak player");
                collision.gameObject.GetComponent<PlayerHealthUnit>().GetDamage(enemyDamage);
                towerHitFlash = collision.gameObject.GetComponent<HitFlash>();
                towerHitFlash.Flash();



            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Player"))
            {

                Debug.Log("kenak player");
                Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                PlayerMovement playerMov = collision.gameObject.GetComponent<PlayerMovement>();
                if (rb)
                {

                    rb.velocity = Vector2.zero;
                    Vector2 thrownDirec = Vector2.zero;
                    rb.simulated = true;
                    if (collision.gameObject.transform.position.x < this.gameObject.transform.position.x)
                    {
                        thrownDirec = new Vector2(-1, 1);
                    }
                    else if (collision.gameObject.transform.position.x > this.gameObject.transform.position.x)
                    {
                        thrownDirec = new Vector2(1, 1);
                    }
                    else if (collision.gameObject.transform.position.y > this.gameObject.transform.position.y)
                    {
                        thrownDirec = new Vector2(1, 1);
                    }

                    playerHitFlash = collision.gameObject.GetComponent<HitFlash>();
                    playerHitFlash.Flash();
                    StartCoroutine(ApplyKnockback(rb, thrownDirec, playerMov));
                }


            }
        }

    }

    private IEnumerator ApplyKnockback(Rigidbody2D rb, Vector2 direction, PlayerMovement playermov)
    {
        rb.velocity = Vector2.zero; // Atur kecepatan ke nol
                                    //rb.AddForce(direction * throwForce, ForceMode2D.Impulse);
        playermov.isKnocked = true;
        rb.velocity = direction * throwForce;


        yield return new WaitForSeconds(0.1f); // Tunggu sejenak agar knockback terasa
        playermov.isKnocked = false;
    }



}
