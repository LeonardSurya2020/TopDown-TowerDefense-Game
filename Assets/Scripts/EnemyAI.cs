using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAI : MonoBehaviour
{
    public GameObject playerObject;
    public Transform player; // Referensi ke pemain
    public Transform[] towerPoints; // Array untuk titik Tower
    public float chaseDistance = 10f; // Jarak untuk mulai mengejar pemain
    public float towerDiveDistance = 10f;
    public float attackDistance = 5f; // Jarak untuk mulai menyerang
    public float speed = 10f; // Kecepatan pergerakan musuh
    public float patrolRadius = 5f; // Radius maksimal untuk patrol point
    private Vector2[] patrolPoints; // Array untuk titik patrol

    public bool isKnocked;

    private int currentPatrolIndex = 0; // Indeks titik patrol saat ini
    private Rigidbody2D rb; // Referensi ke Rigidbody2D musuh
    private IEnemyState currentState;
    public float scale;
    public bool isAttacking = true;
    // Referensi ke state yang berbeda
    public IdleState idleState = new IdleState();
    public PatrolState patrolState = new PatrolState();
    public RunningState runningState = new RunningState();
    public FrontAttackState frontAttackState = new FrontAttackState();
    public Animator animator;

    List<Transform> validTower;

    public AttackArea attackArea;

    public bool isNotGoblinPatrol;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<Transform>();
        if(!isNotGoblinPatrol)
        {
            // Mencari semua objek dengan tag "PatrolPoint" kemudian ambil posisi mereka
            GameObject[] patrolObjects = GameObject.FindGameObjectsWithTag("PatrolPoint");
            List<Vector2> validPatrolPoints = new List<Vector2>();

            // Pilih titik patrol yang berada dalam radius tertentu
            foreach (GameObject patrolObject in patrolObjects)
            {
                float distanceToPoint = Vector2.Distance(transform.position, patrolObject.transform.position);
                if (distanceToPoint <= patrolRadius) // Pastikan titik berada dalam radius patrol
                {
                    validPatrolPoints.Add(patrolObject.transform.position);
                }
            }

            // Simpan titik yang valid dalam array patrolPoints
            patrolPoints = validPatrolPoints.ToArray();
        }
        //else
        //{
        //    // Mencari semua objek dengan tag "PatrolPoint" kemudian ambil posisi mereka
        //    GameObject[] towerObjects = GameObject.FindGameObjectsWithTag("Tower");
        //    validTower = new List<Transform>();

        //    // Pilih titik patrol yang berada dalam radius tertentu
        //    foreach (GameObject towerObject in towerObjects)
        //    {
        //        float distanceToPoint = Vector2.Distance(transform.position, towerObject.transform.position);
        //        if (distanceToPoint <= patrolRadius) // Pastikan titik berada dalam radius patrol
        //        {
        //            validTower.Add(towerObject.transform);
        //        }
        //    }
        //    // Simpan titik yang valid dalam array patrolPoints
        //    towerPoints = validTower.ToArray();
        //}

        // Mulai dengan state Idle
        TransitionToState(idleState);
    }

    void Update()
    {

        if(isNotGoblinPatrol)
        {
            // Mencari semua objek dengan tag "PatrolPoint" kemudian ambil posisi mereka
            GameObject[] towerObjects = GameObject.FindGameObjectsWithTag("Tower");
            validTower = new List<Transform>();

            if (towerObjects.Length <= 0)
            {
                Debug.Log("masuk ke castle");
                towerObjects = GameObject.FindGameObjectsWithTag("Castle");
            }

            // Pilih titik patrol yang berada dalam radius tertentu
            foreach (GameObject towerObject in towerObjects)
            {
                float distanceToPoint = Vector2.Distance(transform.position, towerObject.transform.position);
                if (distanceToPoint <= patrolRadius) // Pastikan titik berada dalam radius patrol
                {
                    validTower.Add(towerObject.transform);
                }
            }
            // Simpan titik yang valid dalam array patrolPoints
            towerPoints = validTower.ToArray();
        }
        currentState.UpdateState(this);  // Update state aktif
    }

    public void TransitionToState(IEnemyState newState)
    {
        currentState?.ExitState(this);   // Panggil ExitState state lama
        currentState = newState;
        currentState.EnterState(this);   // Masuk ke state baru
    }

    // Fungsi untuk Patroling
    public void Patrol()
    {
        // Jika tidak ada patrol points yang valid, keluar dari fungsi
        if (patrolPoints.Length == 0) return;

        // Tentukan posisi target patrol berdasarkan indeks saat ini
        Vector2 targetPosition = patrolPoints[currentPatrolIndex];
       
        // Tambahkan sedikit toleransi jarak agar tidak terlalu sensitif
        float distanceToTarget = Vector2.Distance(transform.position, targetPosition);
        if (distanceToTarget < 0.5f)  // Jika sudah cukup dekat dengan titik patrol, pindah ke titik berikutnya
        {
            // Perbarui indeks patrol dengan cara berputar (looping)
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
        else
        {
            // Gerakkan musuh menuju titik patrol dengan kecepatan yang halus
            Vector2 moveDirection = (targetPosition - (Vector2)transform.position).normalized;
            animator.SetFloat("Speed", MathF.Abs(moveDirection.magnitude));
            Flip(moveDirection.x);
            rb.velocity = moveDirection * speed;  // Gunakan velocity untuk pergerakan mulus
        }
    }

    // Fungsi untuk mengejar pemain
    public void ChasePlayer()
    {
        if(isKnocked)
        {
            return;
        }
        // Mengejar pemain jika jarak lebih dekat dari jarak chase
        if (Vector2.Distance(transform.position, player.position) > chaseDistance) return; // Jangan mengejar jika terlalu jauh

        Vector2 moveDirection = (player.position - transform.position).normalized;
        animator.SetFloat("Speed", MathF.Abs(moveDirection.magnitude));
        Flip(moveDirection.x);
        rb.velocity = moveDirection * speed;  // Gunakan velocity untuk pergerakan mulus menuju pemain
    }

    public void GoToTower()
    {
        foreach (Transform towerObject in towerPoints)
        {
            // Jika jarak ke tower lebih jauh dari towerDiveDistance, lewati titik ini
            if (Vector2.Distance(transform.position, towerObject.position) > towerDiveDistance) continue;

            // Hitung arah pergerakan
            Vector2 moveDirection = (towerObject.position - transform.position).normalized;

            // Atur animasi dan orientasi musuh
            animator.SetFloat("Speed", Mathf.Abs(moveDirection.magnitude));
            Flip(moveDirection.x);

            // Tentukan velocity untuk bergerak menuju tower
            rb.velocity = moveDirection * speed;  // Gunakan velocity untuk pergerakan mulus menuju titik tower yang terdekat
            break; // Keluar dari loop setelah menemukan satu tower dalam jarak yang diinginkan
        }


    }

    // Fungsi untuk menyerang pemain
    public void AttackPlayer()
    {
        if(isAttacking)
        {
            // Menyerang pemain jika jarak lebih dekat dari jarak attack
            StartCoroutine(AttackAnimation());
        }

    }

    public void AttackTower()
    {
        if (isAttacking)
        {
            rb.velocity = Vector2.zero;
            // Menyerang pemain jika jarak lebih dekat dari jarak attack
            StartCoroutine(TowerAttackAnimation());
        }
    }

    public void Flip(float direction)
    {
        if((direction < 0))
        {
            Vector3 localScale = transform.localScale;
            localScale.x = -1f;
            scale = localScale.x;
            transform.localScale = localScale;
        }
        else if((direction > 0))
        {
            Vector3 localScale = transform.localScale;
            localScale.x = 1f;
            scale = localScale.x;
            transform.localScale = localScale;
        }
    }

    public IEnumerator AttackAnimation()
    {
        isAttacking = false;
        float xDistance = transform.position.x - player.position.x;
        if (Vector2.Distance(transform.position, player.position) <= attackDistance)
        {
            
            Vector2 directionToPlayer = player.position - transform.position;

            if (Mathf.Abs(directionToPlayer.x) > Mathf.Abs(directionToPlayer.y))
            {
                if (directionToPlayer.x > 0) // Serangan ke kanan
                {
                    animator.SetTrigger("Front");
                }
                else // Serangan ke kiri
                {
                    animator.SetTrigger("Front");
                }
            }
            else
            {
                if (directionToPlayer.y > 0) // Serangan ke atas
                {
                    animator.SetTrigger("Up");
                }
                else // Serangan ke bawah
                {
                    animator.SetTrigger("Down");
                }
            }


            // Logika serangan disini (misalnya, pengurangan HP pemain)
        }
        yield return new WaitForSeconds(1f);
        isAttacking = true;
    }

    public IEnumerator TowerAttackAnimation()
    {
        foreach(Transform towerpoint in towerPoints)
        {
            isAttacking = false;
            rb.velocity = Vector2.zero;
            float xDistance = transform.position.x - towerpoint.position.x;
            if (Vector2.Distance(transform.position, towerpoint.position) <= attackDistance)
            {
                rb.velocity = Vector2.zero;
                Vector2 directionToPlayer = towerpoint.position - transform.position;

                if (Mathf.Abs(directionToPlayer.x) > Mathf.Abs(directionToPlayer.y))
                {
                    if (directionToPlayer.x > 0) // Serangan ke kanan
                    {
                        animator.SetTrigger("Front");
                    }
                    else // Serangan ke kiri
                    {
                        animator.SetTrigger("Front");
                    }
                }
                else
                {
                    if (directionToPlayer.y > 0) // Serangan ke atas
                    {
                        animator.SetTrigger("Up");
                    }
                    else // Serangan ke bawah
                    {
                        animator.SetTrigger("Down");
                    }
                }


                // Logika serangan disini (misalnya, pengurangan HP pemain)
            }
            yield return new WaitForSeconds(1f);
            isAttacking = true;
            
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, towerDiveDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);
    }

}
