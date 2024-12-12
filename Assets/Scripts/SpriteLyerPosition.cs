using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLyerPosition : MonoBehaviour
{

    private GameObject player;
    private SpriteRenderer spriteRenderer;
    public GameObject positionObject;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y > positionObject.transform.position.y)
        {
            spriteRenderer.sortingOrder = 13;
        }
        else if(player.transform.position.y < this.transform.position.y)
        {
            spriteRenderer.sortingOrder = 11;
        }
    }
}
