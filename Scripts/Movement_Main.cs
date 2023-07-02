using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Movement_Main : MonoBehaviour
{
    //Variables jugador.

    [SerializeField] private float velocidadMovimiento; //Velocidad
    [SerializeField] private Vector2 direccion; //Direccion movimiento
    //[SerializeField] private AudioClip caminarSonido; futuro sonido
    private Rigidbody2D myRb;
    private float MoviX; //Lateral
    private float MoviY; //Vertical


    //Seccion de ataque.
    public GameObject weaponPrefab; //Genera espada y permite ataques
    public Transform weaponSlot; //Espacio del arma
    private bool canAttack = true; //Ataque no spameable
    public float attackDelay = 10f; //Delay de ataque

    private float currentTime;
    private Animator animator;
    //AudioSource audio; futuro sonido
    public bool pantanoVel=false; //Velocidad en el pantano
    int p=0;

    //Estadisticas.
    public Text Velocidad; 
    public Text Daño;
    public Text Escence;

    void Start()
    {
        animator = GetComponent<Animator>();
        myRb = GetComponent<Rigidbody2D>();
        //audio = GetComponent<AudioSource>(); futuro sonido
    }

    void Update()
    {
        //Textos del canvas que marcan estadisticas.
        Velocidad.text = "Velocidad " + velocidadMovimiento;
        Daño.text = "Daño " + GameManager.Instance.damagedone;
        Escence.text = "Escencias "+ GameManager.Instance.points; 

        //Movimiento del jugador.
        MoviX = Input.GetAxisRaw("Horizontal");
        MoviY = Input.GetAxisRaw("Vertical");

        animator.SetFloat("MovX", MoviX);
        animator.SetFloat("MovY", MoviY);

        if (MoviX != 0 || MoviY != 0)
        {
            animator.SetFloat("UltX", MoviX);
            animator.SetFloat("UltY", MoviY);
        }

        direccion = new Vector2(MoviX, MoviY).normalized;

        currentTime += Time.deltaTime;

        //Teclas que permiten los ataques y bloquea el ataque en la cueva (nivel 0).
        if (Input.GetKeyDown(KeyCode.J) && canAttack && SceneManager.GetActiveScene().buildIndex != 2)
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.K) && canAttack && SceneManager.GetActiveScene().buildIndex != 2)
        {
            AttackRotate();    
        }
    }

    //Atques basico
    private void Attack()
    {
        canAttack = false;
        if (MoviX >= 0.0f) 
        {
            animator.SetTrigger("AttackDer");
            GameObject weapon = Instantiate(weaponPrefab, weaponSlot.position, weaponSlot.rotation);
            weapon.transform.parent = weaponSlot;
        }
        else if (MoviX < 0.0f)
        {
            animator.SetTrigger("AttackIzq");
            GameObject weapon = Instantiate(weaponPrefab, weaponSlot.position - new Vector3(1.5f, 0, 0), weaponSlot.rotation); 
            weapon.transform.parent = weaponSlot;
        }

        Invoke("EnableAttack", attackDelay);
    }

    //Ataque especial
    private void AttackRotate()
    {
        canAttack = false;
        if (MoviX >= 0)
        {
            animator.SetTrigger("AttackDer");
            GameObject weapon = Instantiate(weaponPrefab, weaponSlot.position, weaponSlot.rotation);
            weapon.transform.parent = weaponSlot;
        }
        if (MoviX <= 0)
        {
            animator.SetTrigger("AttackIzq");
            GameObject weapon = Instantiate(weaponPrefab, weaponSlot.position - new Vector3(1.5f, 0, 0), weaponSlot.rotation);
            weapon.transform.parent = weaponSlot;
        }

        Invoke("EnableAttack", attackDelay);
    }

    //Puede atacar.
    private void EnableAttack()
    {
        canAttack = true;
    }

    //Recibe baja velocidad del pantano.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pantano"))
        {
            pantanoVel = !pantanoVel;
        }
        PantanoChangeVel();
        
        if (collision.gameObject.CompareTag("Botas"))
        {
            velocidadMovimiento += 2;
        }

        if (collision.gameObject.CompareTag("Daño"))
        {
            GameManager.Instance.damagedone += 10;
        }
    }

    private void PantanoChangeVel()
    {
        if (pantanoVel==true&&p==0)
        {
           velocidadMovimiento = velocidadMovimiento -2;
           p = 1;
        }
        else if (pantanoVel == false && p == 1)
        {
          velocidadMovimiento = velocidadMovimiento +2;
          p = 0;
        }
    }

    private void FixedUpdate()
    {
        myRb.MovePosition(myRb.position + direccion * velocidadMovimiento * Time.fixedDeltaTime);
    }
}
