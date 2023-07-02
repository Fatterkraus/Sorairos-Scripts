using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    //Variables de TP
    public Transform teleportTarget;  //Referencia al objeto de destino
    public Transform Player; //Quien se hace TP (posicion del jugador)

    //Permite realizar el TP segun colision del jugador
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            // Teletransportar al jugador al destino
            Player.position = teleportTarget.position;
        }
    }
}
