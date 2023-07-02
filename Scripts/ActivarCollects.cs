using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarCollects : MonoBehaviour
{
    //Activa los coleccionables en el mapa.
    [SerializeField] GameObject collect1;
    [SerializeField] GameObject collect2;
    [SerializeField] GameObject collect3;
    [SerializeField] GameObject collect4;
    [SerializeField] GameObject collect5;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collect1.SetActive(true);
        collect2.SetActive(true);
        collect3.SetActive(true);
        collect4.SetActive(true);
        collect5.SetActive(true);
    }
}
