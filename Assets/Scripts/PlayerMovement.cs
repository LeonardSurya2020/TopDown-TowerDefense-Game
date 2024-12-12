using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public float scale;
    public float yScale;
    
    public float speed;
    private bool isFacingRight;
    public bool isFacingUp;
    public bool isFacingDown;

    public bool isKnocked;

    public Animator animator;

    public bool isAttack;



    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        float horiz = Input.GetAxisRaw("Horizontal");
        float verti = Input.GetAxisRaw("Vertical");
        if (!isAttack)
        {
            if(isKnocked)
            {
                return;
            }
            Walk(horiz, verti);
        }

        if (!isAttack)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                isAttack = true;
                rb.velocity = Vector2.zero;
                StartCoroutine(Attack());
            }
        }

    }

    // Walk function
    private void Walk(float horizontalWalkInput, float verticalWalkInput)
    {
        // Walk Function order by direction input

        Vector2 movement = new Vector2(horizontalWalkInput, verticalWalkInput).normalized * speed;
        animator.SetFloat("Speed", MathF.Abs(movement.magnitude));

        if (Mathf.Abs(horizontalWalkInput) > 0 || Mathf.Abs(verticalWalkInput) > 0)
        {
            if(verticalWalkInput > 0)
            {
                isFacingUp = true;
                isFacingDown = false;
            }
            else if(verticalWalkInput < 0)
            {
                isFacingUp = false;
                isFacingDown = true;
            }

            if (horizontalWalkInput > 0)
            {
                isFacingRight = true;
                isFacingUp = false;
                isFacingDown = false;
            }
            else if (horizontalWalkInput < 0)
            {
                isFacingRight = false;
                isFacingUp = false;
                isFacingDown = false;
            }

            //Debug.Log(isFacingRight);
            Flip(horizontalWalkInput, isFacingRight, verticalWalkInput, isFacingUp);

            // Membuat vektor gerakan yang dinormalisasi
            

           
            rb.velocity = movement;
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }

    }

    private IEnumerator Attack()
    {
        if(isFacingUp)
        {
            animator.SetTrigger("Up");
            yield return new WaitForSeconds(0.5f);
            isAttack = false;
        }
        else if(isFacingDown)
        {
            animator.SetTrigger("Down");
            yield return new WaitForSeconds(0.5f);
            isAttack = false;

        }
        else
        {
            animator.SetTrigger("Front");
            yield return new WaitForSeconds(0.5f);
            isAttack = false;
        }
        
    }

    private void Flip(float walkinputH, bool facingRight, float walkInputV, bool facingUp)
    {
        if(facingUp && walkInputV > 0)
        {
            Vector3 localScale = transform.localScale;
            localScale.y = 1f;
            yScale = localScale.y;
            transform.localScale = localScale;
        }
        else if(!facingUp && walkInputV < 0 && isFacingDown)
        {
            Vector3 localScale = transform.localScale;
            localScale.y = 1f;
            yScale = localScale.y;
            transform.localScale = localScale;
        }
        

        if (facingRight && walkinputH > 0)
        {
            Vector3 localScale = transform.localScale;
            localScale.x = 1f;
            scale = localScale.x;
            yScale = 0;
            transform.localScale = localScale;
        }
        else if (facingRight == false && walkinputH < 0)
        {
            Vector3 localScale = transform.localScale;
            localScale.x = -1f;
            scale = localScale.x;
            yScale = 0;
            transform.localScale = localScale;
        }
    }
}
