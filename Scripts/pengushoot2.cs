using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pengushoot2 : MonoBehaviour
{
    //Variables pinguino
    [SerializeField] GameObject follow; //Mirar al jugador y seguirlo.
    public Transform player; //Ubiacion del jugador.
    [SerializeField] float distToAttack; //Distancia dispara de bola.
    [SerializeField] float attackVel; //Velocidad disparo de bola.
    [SerializeField] float closestDist; //Distancia que tan cerca llega a disparar.
    [SerializeField] float lerpSpeedRotation; 
    Rigidbody2D rb;
    Vector3 enemyDirection;
    Animator anim;
    private bool isFacingRight = true; //Derecha o izquierda segun jugador.
    public GameObject weaponPrefab; //Bola de nieve 1.
    public GameObject weaponPrefab2; //Bola de nieve 2.
    public Transform weaponSlot; //Posicion que dispara la bola.
    public float attackDelay = 10f; //Tiempo que tarda en disparar.
    private float currentTime; 
    private GameObject bala; //Bola que dispara (prefab de la nieve).


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    private void Update()
    {
        anim.SetBool("Idle", true);

        //Movimiento y ataque del pinguino.
        currentTime += Time.deltaTime;
        enemyDirection = follow.transform.position - transform.position;
        if (enemyDirection.magnitude < distToAttack && enemyDirection.magnitude > closestDist)
        {
            if (currentTime >= attackDelay)
            {
                Instantiate(bala, weaponSlot.position, transform.rotation);
                transform.right = Vector3.Lerp(transform.right, enemyDirection, lerpSpeedRotation * Time.deltaTime);
                currentTime = 0;
                anim.SetTrigger("Attack");
            }
        }

        if (transform.position.x < player.position.x && !isFacingRight)
        {
            bala = weaponPrefab2;
            Flip();
            GameManager.Instance.isRightbull2 = true;
        }
        else if (transform.position.x > player.position.x && isFacingRight)
        {
            bala = weaponPrefab;
            Flip();
            GameManager.Instance.isRightbull2 = false;
        }
    }

    //Radio para detectar jugador mediante GizMos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distToAttack);
    }

    //Giro del enemigo
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    
}
