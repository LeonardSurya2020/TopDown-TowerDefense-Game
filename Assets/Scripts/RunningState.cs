using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : IEnemyState
{
    public void EnterState(EnemyAI enemy)
    {
        // Setup state Running jika perlu
        Debug.Log("Masuk ke state Running");
    }

    public void UpdateState(EnemyAI enemy)
    {
        if(enemy.isNotGoblinPatrol == false)
        {
            // Cek jarak ke pemain, jika cukup dekat, musuh akan menyerang
            float distanceToPlayer = Vector2.Distance(enemy.transform.position, enemy.player.position);

            if (distanceToPlayer <= enemy.attackDistance)
            {
                enemy.TransitionToState(enemy.frontAttackState);  // Beralih ke serangan
            }
            else if (distanceToPlayer > enemy.chaseDistance)
            {
                enemy.TransitionToState(enemy.idleState);  // Kembali ke idle jika pemain menjauh
            }
            else
            {
                enemy.ChasePlayer();  // Mengejar pemain
            }
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
                enemy.TransitionToState(enemy.frontAttackState);
            }
            else if(!isTowerInRange && enemy.isKnocked == false)
            {
                enemy.GoToTower();
            }
        }

    }

    public void ExitState(EnemyAI enemy)
    {
        // Cleanup jika diperlukan
        Debug.Log("Keluar dari state Running");
    }
}
