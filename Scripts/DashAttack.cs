using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttack : MonoBehaviour
{
    //Variables escarabajo.
    public float dashForce = 10f;
    public float dashDuration = 0.5f;
    private Animator animator;
    private Rigidbody2D rb;
    private bool isDashing = false;
    private float dashTimer = 0f;
    public Transform followTarget;
    private Transform player;
    public float maxFollowDistance = 3f;
    [SerializeField] int damage;
    private float currentTime;
    [SerializeField] float attackVel;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player").transform; //Sigue al jugador
        animator = GetComponent<Animator>();
        animator.SetBool("Walking", true); //animacion
    }

    private void Update()
    {
        if (isDashing)
        {
            currentTime += Time.deltaTime; //daño
            dashTimer += Time.deltaTime; //dash
            if (dashTimer >= dashDuration)
            {
                isDashing = false;
                dashTimer = 0f;
                // Realizar el ataque o acciones posteriores al dash
            }
        }
        else
        {
            if (followTarget != null && Vector2.Distance(transform.position, followTarget.position) <= maxFollowDistance)
            {
                PerformDashAttack();
            }
            // Calcular la direccion hacia el jugador
            Vector2 direction = ( transform.position-player.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Rotar el enemigo hacia el jugador
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    //Colision jugador + ataque.
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (currentTime >= attackVel)
        {
            if (collision.gameObject.layer == 3)
            {
                collision.gameObject.GetComponent<Life>().GetDamage(damage);
                animator.SetBool("Walking", false);
                animator.SetTrigger("Attack");
                currentTime = 0;
            }
        }
    }
    
    //El movimiento en dash
    public void PerformDashAttack()
    {
        if (!isDashing)
        {
            isDashing = true;
            rb.velocity = (followTarget.position - transform.position).normalized * dashForce;
        }
    }

    //Radio de deteccion por gizmos.
    private void OnDrawGizmos()
    {
        if (followTarget != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, maxFollowDistance);
        }
    }
}
