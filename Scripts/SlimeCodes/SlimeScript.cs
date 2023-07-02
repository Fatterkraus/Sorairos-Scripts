using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    //Variables del slime
    [SerializeField] GameObject follow; //Seguir objeto
    public Transform player; //Seguir posicion del jugador.
    [SerializeField] float distToAttack; //Distancia de ataque
    [SerializeField] float attackVel; //Velocidad de ataque
    [SerializeField] float closestDist; //Distancia a la que permite moverse
    [SerializeField] float lerpSpeedRotation; //Rotar enemigo
    Rigidbody2D rb; 
    Vector3 enemyDirection;
    Animator anim;
    private bool isFacingRight = true; //Derecha o no.
    private bool isAttacking=false; //Sino ataca que este quieto
    private float currentTime;
    [SerializeField] int damage; //Daño.

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    private void FixedUpdate()
    {
        rb.velocity = enemyDirection.normalized * attackVel;
    }

    //Movimiento del Slime
    private void Update()
    {

        enemyDirection = follow.transform.position - transform.position;
        anim.SetBool("Walking", true);

        currentTime += Time.deltaTime;

        transform.right = Vector3.Lerp(transform.right, enemyDirection, lerpSpeedRotation * Time.deltaTime);
        if (enemyDirection.magnitude > distToAttack || enemyDirection.magnitude < closestDist)
        {
            enemyDirection = Vector3.zero;
            anim.SetBool("Walking", false);
        }
        if (transform.position.x < player.position.x && !isFacingRight)
        {
            Flip();
        }
        else if (transform.position.x > player.position.x && isFacingRight)
        {
            Flip();
        }
    }

    //Radio que detecta al jugador usando un Gizmos.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distToAttack);
    }

    //Colision enemigo y ataque del mismo quitando vida.
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (currentTime >= attackVel)
        {
            if (collision.gameObject.layer == 3)
            {
                collision.gameObject.GetComponent<Life>().GetDamage(damage); //Saca vida al componente vida del jugador.
                anim.SetTrigger("Attack"); //Animacion ataque
                currentTime = 0;
            }
        }
    }

    //Giro.
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
