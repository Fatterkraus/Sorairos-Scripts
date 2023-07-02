using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloqueo : MonoBehaviour
{
    //Colision del bloqueo de piedras en el primer nivel
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Escena de victoria
        if (collision.gameObject.layer == 3 && GameManager.Instance.contador==1) //NewZone.
        {
            Destroy(gameObject);
        }
    }
}
