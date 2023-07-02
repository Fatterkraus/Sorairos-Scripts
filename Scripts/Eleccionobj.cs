using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eleccionobj : MonoBehaviour
{
    //Permite elegir cual bloqueo quitar de la zona segura en el nivel 6
    [SerializeField] GameObject piedra2trigg;
    [SerializeField] GameObject piedra1trigg;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Item segun la piedra
        if (collision.gameObject.layer == 3 )
        {
            piedra2trigg.SetActive(false);
            Destroy(piedra1trigg);
        }
    }

}
