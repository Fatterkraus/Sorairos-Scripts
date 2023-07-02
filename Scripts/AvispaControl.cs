using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvispaControl : MonoBehaviour
{
    //Mismo script que el Attack on View solo que con veneno.

    [SerializeField] GameObject follow;
    public Transform player;
    [SerializeField] float distToAttack;
    [SerializeField] float attackVel;
    [SerializeField] float closestDist;
    [SerializeField] float lerpSpeedRotation;
    Rigidbody2D rb;
    Vector3 enemyDirection;
    Animator anim;
    private bool isFacingRight = true;
    public GameObject objetoVeneno; 
    private float currentTime;
    private bool veneno = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        rb.velocity = enemyDirection.normalized * attackVel;
    }

    private void Update()
    {
        enemyDirection = follow.transform.position - transform.position;
        anim.SetBool("Flying", true);

        currentTime += Time.deltaTime;

        transform.right = Vector3.Lerp(transform.right, enemyDirection, lerpSpeedRotation * Time.deltaTime);
        if (enemyDirection.magnitude > distToAttack || enemyDirection.magnitude < closestDist)
        {
            enemyDirection = Vector3.zero;
            anim.SetBool("Flying", false);
        }
        if (transform.position.x < player.position.x && !isFacingRight)
        {
            Flip();
        }
        else if (transform.position.x > player.position.x && isFacingRight)
        {
            Flip();
        }
        if (veneno == true && currentTime >= attackVel)
        {
            anim.SetTrigger("Attack");
            currentTime = 0;
            veneno = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distToAttack);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si el enemigo colisiona con el veneno, se reduce su salud (activar veneno). 
        if (collision.gameObject.layer == 3)
        {
            veneno = true;
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
