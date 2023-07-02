using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChildLife : MonoBehaviour //Utilizamos este script para indicar vida de cada enemigo.
{
    public float health = 25f; // Vida del enemigo
    private bool isDead = false; // Variable que indica si el enemigo está muerto
    Animator anim;
    public float delay = 2f; // Delay para generar los enemigos hijos
    private GeneratorChilds slime; //Llamamos al codigo Generator Childs.
    public float destroyDelay = 1f; // Retraso antes de destruir el objeto enemigo

    private void Start()
    {
        anim = GetComponent<Animator>();
        slime = GetComponent<GeneratorChilds>();
    }

    //Muerte del enemigo verdadera, animacion de muerte, destruir, sumar punto, sumar escencia y si tiene hijo generarlo.
    private void Update()
    {
        if (health <= 0f && !isDead )
        {
            isDead = true;
            anim.SetTrigger("Death");
            Destroy(gameObject, destroyDelay);
            GameManager.Instance.points = GameManager.Instance.points + 1;
            GameManager.Instance.damagedone = GameManager.Instance.damagedone + 0.1f;
            slime.SpawnChild();
        }
    }

    //Recibir daño
    public void GetDamage(float value)
    {
        health -= value; //currentHealth = currentHealth - value; 
    }
}