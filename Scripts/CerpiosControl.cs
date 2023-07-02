using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerpiosControl : MonoBehaviour
{
    public float movVel;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isFacingRight = true;
    public float tiempoCambio;
    public bool seePlayer = false;
    private float currentTime;
    private float startPositionY;
    public float verticalDistance;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        startPositionY = transform.position.y;
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.up * Mathf.Sin(Time.time * movVel) * verticalDistance; //Movimiento vertical.
    }

    private void Update()
    {
        HandleMovement();
        currentTime += Time.deltaTime;
    }

    //Movimiento vertical y animacion
    private void HandleMovement()
    {
        if (seePlayer == false)
        {
            anim.SetBool("Walking", true);

            if (currentTime >= tiempoCambio)
            {
                Flip();
                currentTime = 0f;
            }
        }
    }
    //Giro
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
