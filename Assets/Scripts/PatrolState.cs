using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    public void EnterState(EnemyAI enemy)
    {
        // Setup atau reset variabel jika diperlukan
        Debug.Log("Masuk ke state Patrol");
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
                enemy.Patrol();  // Musuh patroli
            }
        }
        else
        {
            foreach (Transform towerPoint in enemy.towerPoints)
            {
                // Cek jarak ke pemain, jika dekat maka musuh akan mengejar
                float distanceToPlayer = Vector2.Distance(enemy.transform.position, towerPoint.position);

                if (distanceToPlayer <= enemy.towerDiveDistance)
                {
                    enemy.TransitionToState(enemy.runningState);
                }
                else
                {
                    enemy.Patrol();  // Musuh patroli
                }
            }

        }

    }

    public void ExitState(EnemyAI enemy)
    {
        // Cleanup jika diperlukan
        Debug.Log("Keluar dari state Patrol");
    }
}
