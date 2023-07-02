using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AparecerYDesaparecer : MonoBehaviour
{
    //Llamamos a los textos que se encuentran en el canvas.
    [SerializeField] GameObject Lectura;
    [SerializeField] GameObject Interaccion;

    //Activamos cada uno segun la colision.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Interaccion.SetActive(true); 
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Interaccion.SetActive(false);
        Lectura.SetActive(false);
    }
    
}
