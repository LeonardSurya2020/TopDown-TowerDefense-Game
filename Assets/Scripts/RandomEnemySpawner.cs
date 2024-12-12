using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemySpawner : MonoBehaviour
{
    public GameObject[] randomObject;
    public float radius = 1f;

    public float firstSecondSpawn = 5f;
    public float secondSpawn = 5f;
    public float minTrans;
    public float maxTrans;



    private void Start()
    {
        StartCoroutine(RandomSpawn());
    }

    public IEnumerator RandomSpawn()
    {
        while (true)
        {

            yield return new WaitForSeconds(firstSecondSpawn);
            int randomNumber = Random.Range(0, randomObject.Length);
            Debug.Log("number = " +  randomNumber);
            var position = this.transform.position;
            GameObject gameObj = Instantiate(randomObject[randomNumber], position, Quaternion.identity);
            yield return new WaitForSeconds(secondSpawn);

        }
    }
}
