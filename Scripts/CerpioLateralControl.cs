using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerpioLateralControl : MonoBehaviour
{
    //Variables del fantasma Cerpio.
    public float movVel;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isFacingRight = true;
    public float tiempoCambio;
    private float currentTime;
    private float startPositionX;
    public float horizontalDistance;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        startPositionX = transform.position.x;
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.right * Mathf.Sin(Time.time * movVel) * horizontalDistance; //Movimiento lateral.
    }

    private void Update()
    {
        HandleMovement();
        currentTime += Time.deltaTime;
    }

    //Movimiento lateral y animacion
    private void HandleMovement()
    {
       anim.SetBool("Walking", true);

       if (currentTime >= tiempoCambio)
       {
           Flip();
           currentTime = 0f;
       }
    }

    //Giro del enemigo
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
