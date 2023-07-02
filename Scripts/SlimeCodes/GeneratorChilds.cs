using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorChilds : MonoBehaviour
{
    //Variables.

    public GameObject childEnemy; // Prefab del enemigo hijo
    public float delay = 2f; // Delay para generar los enemigos hijos
    private bool isDead = false; // Variable que indica si el enemigo está muerto
    public Transform spawnpoint; //Spawn del hijo 1
    public Transform spawnpoint2; //Spawn del hijo 2
    public float destroyDelay = 1f; // Retraso antes de destruir el objeto enemigo

    //Generar hijos
    public void SpawnChild()
    {
        Instantiate(childEnemy, spawnpoint.position, transform.rotation);
        Instantiate(childEnemy, spawnpoint2.position, transform.rotation);
    }
}

