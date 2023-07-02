using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float speed = 0;
    public float lifeTime = 5f;
    private float currentTime;
    private float damage=GameManager.Instance.damagedone;

    void Update()
    {
        //Movimiento y tiempo del golpe que genera la espada.
        transform.Translate(speed * Time.deltaTime, 0, 0);
        currentTime += Time.deltaTime;
        if(currentTime > lifeTime)
        {
            Destroy(gameObject);
        }
    }

    //Colision del golpe a los enemigos
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            collision.gameObject.GetComponent<ChildLife>().GetDamage(damage); //Daño a los enemigos llamando su componente vida.
            Destroy(gameObject);
        }
    }
}
