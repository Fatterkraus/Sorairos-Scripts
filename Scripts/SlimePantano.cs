using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePantano : MonoBehaviour
{
    //Variables slime pantano.
    [SerializeField] GameObject follow; //Seguir objeto
    public Transform player; //Posicion del jugador
    [SerializeField] float distToAttack; //Distancia de ataque
    public float movVel; //Velocidad del slime
    [SerializeField] float closestDist; 
    [SerializeField] float lerpSpeedRotation;
    private Rigidbody2D rb;
    Vector3 enemyDirection;
    private Animator anim;
    private bool isFacingRight = true; //Patrullaje derecha o izquierda
    public float contadorT; //Contador para llegar a cambiar de lado.
    public float tiempoCambio; //Tiempo que tarda hasta cambiar de lado (contador debe llegar a este numero)
    public bool esDerecha; //Si es derecha o no girar.
    public bool seePlayer=false; //Ver al jugador sino lo ve es para que siga patrullando.
    [SerializeField] int damage; //Daño
    private float currentTime; 
    [SerializeField] float attackVel; //Velocidad de ataque


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        contadorT = tiempoCambio;
    }

    private void FixedUpdate()
    {
        rb.velocity = enemyDirection.normalized * movVel/1.5f;
    }

    private void Update()
    {
        HandleMovement();
        FollowPlayer();
        currentTime += Time.deltaTime;

    }

    //Seguir al jugador.
    private void FollowPlayer()
    {
        if (enemyDirection.magnitude < distToAttack || enemyDirection.magnitude > closestDist)
        {
            enemyDirection = follow.transform.position - transform.position;
            transform.right = Vector3.Lerp(transform.right, enemyDirection, lerpSpeedRotation * Time.deltaTime);
            anim.SetBool("Walking", true);
            seePlayer = true;
        }

        if (enemyDirection.magnitude > distToAttack || enemyDirection.magnitude < closestDist)
        {
            enemyDirection = Vector3.zero;
            anim.SetBool("Walking", false);
            seePlayer = false;
        }

         if(seePlayer==true)
         {
            if (transform.position.x > player.position.x && !isFacingRight)
            {
                Flip();
                esDerecha=false;
            }
            else if (transform.position.x < player.position.x && isFacingRight)
            {
                Flip();
                esDerecha = true;
            }
         }
    }
    
    //Moverse lateralmente (patrullaje).
    private void HandleMovement()
    {
        if(seePlayer==false)
        {

            if (esDerecha == true)
            {
                 transform.position += Vector3.right * movVel * Time.deltaTime;
                 anim.SetBool("Walking", true);
            }
            if (esDerecha == false)
            {
                transform.position -= Vector3.right * movVel * Time.deltaTime;
                anim.SetBool("Walking", true);
            }

            contadorT -= Time.deltaTime;

            if (contadorT <= 0)
            {
              Flip();
              contadorT = tiempoCambio;
              esDerecha = !esDerecha;
            }
        }
    }

    //Radio de Gizmos en el que permitime saber la distancia entre Slime y jugador.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distToAttack);
    }

    //Colision para realizar el ataque.
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (currentTime >= attackVel)
        {
            if (collision.gameObject.layer == 3)
            {
                collision.gameObject.GetComponent<Life>().GetDamage(damage);
                anim.SetTrigger("Attack");
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

