using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontAttackState : IEnemyState
{
    public void EnterState(EnemyAI enemy)
    {
        // Setup serangan jika perlu
        Debug.Log("Masuk ke state Front Attack");
    }

    public void UpdateState(EnemyAI enemy)
    {
        if(enemy.isNotGoblinPatrol == false)
        {
            // Misalnya, musuh menyerang pemain saat berada dalam jarak tertentu
            Debug.Log("Musuh menyerang pemain!");
            enemy.AttackPlayer();

            // Setelah menyerang, bisa kembali ke state Running atau Idle tergantung jarak
            float distanceToPlayer = Vector2.Distance(enemy.transform.position, enemy.player.position);
            if (distanceToPlayer > enemy.attackDistance)
            {
                enemy.TransitionToState(enemy.runningState);
            }
            //else
            //{
            //    enemy.TransitionToState(enemy.idleState);
            //}
        }
        else
        {
            bool isTowerInRange = false;

            // Iterasi setiap titik di towerPoints
            foreach (Transform towerPoint in enemy.towerPoints)
            {
                float distanceToTower = Vector2.Distance(enemy.transform.position, towerPoint.position);

                // Jika ada satu titik yang berada dalam jarak chaseDistance, ubah state ke runningState
                if (distanceToTower <= enemy.attackDistance)
                {
                    isTowerInRange = true;
                    break;
                }
            }


            if (isTowerInRange)
            {
                Debug.Log("Musuh menyerang Tower!");
                enemy.AttackTower();
            }
            else
            {
                enemy.TransitionToState(enemy.idleState);
            }
        }

    }

    public void ExitState(EnemyAI enemy)
    {
        // Cleanup setelah menyerang jika perlu
        Debug.Log("Keluar dari state Front Attack");
    }


}
