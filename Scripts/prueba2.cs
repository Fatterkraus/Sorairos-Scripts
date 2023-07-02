using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prueba2 : MonoBehaviour //Se llama asi porque probamos cosas y no se cambio.
{
    //Variables del codigo.
    private Collider2D z_Collider;  //Colision del objeto que podes clickear
    [SerializeField] GameObject Lectura; //Texto que sale para leer
    [SerializeField] GameObject Collect; //Activa coleccionables
    [SerializeField] GameObject Interaccion; //Texto que sale para interactuar
    [SerializeField]
    private ContactFilter2D z_Filter; //Filtro de lo que tiene que chocar para activarse.
    private List<Collider2D> z_CollidedObjects = new List<Collider2D>(1);

    
    void Start()
    {
        z_Collider = GetComponent<Collider2D>();
    }

    //Generamos la tecla H y llamamos a los textos segun la colision.
    void Update()
    {
        z_Collider.OverlapCollider(z_Filter, z_CollidedObjects);
        foreach(var o  in z_CollidedObjects)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                Lectura.SetActive(true);
                Collect.SetActive(true);
                Interaccion.SetActive(false);
            }
        }
    }
}
