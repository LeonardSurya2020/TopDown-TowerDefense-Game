using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{
    public void EnterState(EnemyAI enemy)
    {
        Debug.Log("masuk ke state Idle");
    }

    public void UpdateState(EnemyAI enemy)
    {

        if(enemy.isNotGoblinPatrol == false)
        {
            // Cek jarak ke pemain, jika dekat maka musuh akan mengejar
            float distanceToPlayer = Vector2.Distance(enemy.transform.position, enemy.player.position);

            if (distanceToPlayer <= enemy.chaseDistance)
            {
                enemy.TransitionToState(enemy.runningState);
            }
            else
            {
                enemy.TransitionToState(enemy.patrolState);
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
                if (distanceToTower <= enemy.towerDiveDistance)
                {
                    isTowerInRange = true;
                    break;
                }
            }

            if (isTowerInRange)
            {
                enemy.TransitionToState(enemy.runningState);
            }
            else
            {
                enemy.TransitionToState(enemy.patrolState);
            }
        }

    }

    public void ExitState(EnemyAI enemy)
    {
        Debug.Log("Keluar dari state Idle");
    }
}
