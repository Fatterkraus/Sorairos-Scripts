using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaBack : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Escena de victoria
        if (collision.gameObject.layer == 3) //NewZone.
        {
            SceneManager.LoadScene(3);
        }
    }
}
