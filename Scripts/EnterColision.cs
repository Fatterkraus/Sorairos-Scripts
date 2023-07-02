using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnterColision : MonoBehaviour
{
    //Colision para animación.
    [SerializeField] GameObject firstMenu;
    GameObject currentMenu;
    [SerializeField] GameObject _texto;

    void Start()
    {
        currentMenu = firstMenu;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            firstMenu.SetActive(true);
        }
        if (collision.gameObject.layer == 12)
        {
            firstMenu.SetActive(false);
        }
    }
}
