using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionItems : MonoBehaviour
{
   //Items se destruyen si colisionan con el jugador
   private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
