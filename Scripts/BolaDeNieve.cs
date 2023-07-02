using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaDeNieve : MonoBehaviour
{
    public float speed = 0; 
    public float lifeTime = 5f;
    private float currentTime;
    [SerializeField] int damage;

    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);

        //Destruir bola de nieve si pasa el tiempo.
        currentTime += Time.deltaTime;
        if (currentTime > lifeTime)
        {
            Destroy(gameObject);
        }
    }

    //Colision con jugador.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            collision.gameObject.GetComponent<Life>().GetDamage(damage); //Daño al jugador llamando su componente vida.
            Destroy(gameObject);
        }
    }
}
