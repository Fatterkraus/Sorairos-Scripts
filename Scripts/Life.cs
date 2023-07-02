using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Life : MonoBehaviour
{
    //Variables de vida.

    public float maxHealth = 100; //Maximo de vida
    public float damageCooldown = 1f; //Daño
    public float currentHealth; //Vida que llevas en el juego
    private SpriteRenderer spriteRenderer; //Renderizado barra
    public Image lifeImage; //Imagen barra
    private float currentTime;
    public int v = 0; //Veneno
    public bool veneno = false; //Activa o desactiva veneno
    public event Action OnDeath; //Muerte del jugador como evento.
    public Text Vida; //Texto del canvas que implica la vida (estadisticas UI)

    private void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        //Daño
        if (currentHealth <= 0)
        {
            Die();
        } 
        //Veneno
        if (veneno == true && currentTime >= damageCooldown)
        {
            if (v <= 3)
            {
                v += 1;
                currentTime = 0;
                currentHealth = currentHealth - 10;
                if (currentHealth <= 0)
                {
                    Die();
                }
            }
            if (v >= 4)
            {
                v = 0;
                veneno = false;
            }
        }

        lifeImage.fillAmount=currentHealth/maxHealth;
        Vida.text = "Vida " + currentHealth;
    }

    //Recibe veneno
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 20)
        {
            veneno=true;
        }

        //Daño de cactus.
        if (collision.gameObject.layer == 17)
        {
            currentHealth = currentHealth - 1;
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    //Recibe vida
    private void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.gameObject.layer == 11)//hit
        {
            maxHealth= maxHealth +200;
            currentHealth= currentHealth + 200;
        }
    }

    //Recibie daño
    public void GetDamage(int value)
    {
        currentHealth -= value; //currentHealth = currentHealth - value; 
    }
    
    //Muere
    private void Die()
    {
        Destroy(gameObject);
        OnDeath?.Invoke();
    }
}