using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyComport : MonoBehaviour
{
    //Variables enemigo basicos.
    public float speed;
    public bool esDerecha;
    public float contadorT;
    public float tiempoCambio;
    public Rigidbody2D rbd;
    public float attackRange;
    Animator anim;
    [SerializeField] int damage;
    private float currentTime;
    [SerializeField] float attackVel;
    public float distancia; //lejania del objetivo

    void Start()
    {
        contadorT = tiempoCambio;
        anim = GetComponent<Animator>();
    }

    // Update para movimientos
    void Update()
    {
        currentTime += Time.deltaTime;

        if (esDerecha == true)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            transform.localScale = new Vector3(-0.1316245f, 0.1147255f, 0.3149078f);
            anim.SetBool("RWK", true);

        }
        if (esDerecha == false)
        {
            transform.position -= Vector3.right * speed * Time.deltaTime;
            transform.localScale = new Vector3(0.1316245f, 0.1147255f, 0.3149078f);
            anim.SetBool("LWK", true);
        }

        contadorT -= Time.deltaTime;

        if (contadorT <= 0)
        {
            contadorT = tiempoCambio;
            esDerecha = !esDerecha;
        }
    }

    //Colision de ataque al jugador
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

    public void OnDeath()
    {
        Destroy(gameObject);
    }
}
