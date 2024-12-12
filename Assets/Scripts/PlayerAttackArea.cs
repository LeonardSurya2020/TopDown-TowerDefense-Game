using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackArea : MonoBehaviour
{
    public float playerDamage = 2;
    //private Health health;
    private PlayerMovement playerMovement;
    private HitFlash enemyHitFlash;
    public float throwForce = 10f;



    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("PatrolEnemy") || collision.gameObject.CompareTag("TowerEnemy"))
        {

            Debug.Log("kenak player");
            collision.gameObject.GetComponent<EnemyHealthUnit>().GetDamage(playerDamage);
            EnemyAI enemyMov = collision.gameObject.GetComponent<EnemyAI>();
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb)
            {

                rb.velocity = Vector2.zero;
                Vector2 thrownDirec = Vector2.zero;
                rb.simulated = true;
                Vector2 directionToPlayer = collision.gameObject.transform.position - transform.position;

                if (Mathf.Abs(directionToPlayer.x) > Mathf.Abs(directionToPlayer.y))
                {
                    if (directionToPlayer.x > 0) // Serangan ke kanan
                    {
                        thrownDirec = new Vector2(1, 0);
                    }
                    else // Serangan ke kiri
                    {
                        thrownDirec = new Vector2(-1, 0);
                    }
                }
                else
                {
                    if (directionToPlayer.y > 0) // Serangan ke atas
                    {
                        thrownDirec = new Vector2(0, 1);
                    }
                    else // Serangan ke bawah
                    {
                        thrownDirec = new Vector2(0, -1);
                    }
                }
                //if (collision.gameObject.transform.position.x < this.gameObject.transform.position.x)
                //{
                //    thrownDirec = new Vector2(-1, 0);
                //}
                //else if (collision.gameObject.transform.position.x > this.gameObject.transform.position.x)
                //{
                //    thrownDirec = new Vector2(1, 0);
                //}
                //else if (collision.gameObject.transform.position.y > this.gameObject.transform.position.y)
                //{
                //    thrownDirec = new Vector2(1, 1);
                //}
                enemyHitFlash = collision.gameObject.GetComponentInChildren<HitFlash>();
                enemyHitFlash.Flash();
                StartCoroutine(ApplyKnockBackEnemy(rb, thrownDirec, enemyMov));
            }


        }
    }

    private IEnumerator ApplyKnockBackEnemy(Rigidbody2D rb, Vector2 direction, EnemyAI enemyMov)
    {
        enemyMov.isKnocked = true;
        rb.velocity = Vector2.zero; // Atur kecepatan ke nol
                                    //rb.AddForce(direction * throwForce, ForceMode2D.Impulse);
        rb.velocity = direction * throwForce;


        yield return new WaitForSeconds(0.7f); // Tunggu sejenak agar knockback terasa
        enemyMov.isKnocked = false;

    }
}
