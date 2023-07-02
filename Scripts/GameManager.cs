using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Variables que usaremos.

    public static GameManager Instance;
    public int contador = 0;
    public bool isRightbull=false;
    public bool isRightbull2=false;
    public float damagedone = 10; //Daño que hace el personaje a los enemigos.
    public float damagecharcdone = 10; //Daño que le hacen al personaje.
    [SerializeField] GameObject player; //Personaje
    public int collectable = 0; //Coleccionables
    public Text textocount; //Texto de pergaminos
    public float points = 0; //Texto de puntos

    //Usamos para las instancias y muerte del jugador.
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        player.GetComponent<Life>().OnDeath += FinishGame;
    }

    private void Update()
    {
        textocount.text = "Pergaminos " + collectable + " de 5";
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Escena de victoria
        if (collision.gameObject.layer == 9) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        //Escena cinematica lago.
        if (collision.gameObject.layer == 15) 
        {
            //SceneManager.LoadScene(CINEMATICA);
            contador = 1;
        }
    }

    void FinishGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
}
