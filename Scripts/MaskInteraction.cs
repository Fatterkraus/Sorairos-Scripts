using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskInteraction : MonoBehaviour
{
    //Variables para interactuar con mascara. Maximo 3 textos por longitud de guión.
    [SerializeField] GameObject Lectura;
    [SerializeField] GameObject Lectura2;
    [SerializeField] GameObject Lectura3;
    private float currentTime;
    private int i = 0;

    //Contador para los textos (si se pasa de largo desaparece).
    void Update()
    {
        currentTime += Time.deltaTime;


        if (currentTime >= 10)
        {
           currentTime = 0;
           Lectura3.SetActive(false);
        }
    }

    //Activacio por trigger en un bloque invisible a los textos.
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&& i==0)
        {
            //Primera interaccion.
            Lectura.SetActive(true);

            //Segunda interaccion.
            if (currentTime >= 5)
            {
                 Lectura.SetActive(false);
                 Lectura2.SetActive(true);
            }

            //Tercera interaccion.
            if (currentTime >= 8)
            {
                 Lectura2.SetActive(false);
                 Lectura3.SetActive(true);
                i = 1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentTime = 0;
    }
}
