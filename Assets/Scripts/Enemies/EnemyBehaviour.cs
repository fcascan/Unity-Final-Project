using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : MonoBehaviour
{


    [SerializeField] private GameObject dropPrefab;
    private Animator animator;
    public Rigidbody2D m_Rigidbody2D;
    public Transform fireWarriorPrefab;
    private bool mirandoDerecha = true;
    private bool Camina = true;
    [Header("vida")]
    [SerializeField] private float life;
    [Header("Ataque")]
    [SerializeField] private Transform ControladorAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private float dañoAtaque;
    [SerializeField] private float maxChaseDistance;
    [SerializeField] private float velocidadMovimiento;


    void Start()
    {
        animator = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        fireWarriorPrefab = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
    }

    void FixedUpdate()
    {

            
        float distanciaJugador = Vector2.Distance(transform.position, fireWarriorPrefab.position);
        animator.SetFloat("distanciaJugador", distanciaJugador);
        if (distanciaJugador < maxChaseDistance && Camina== true)
        {
            Camina = false;
            animator.SetBool("Walk", true);
            m_Rigidbody2D.velocity = new Vector2(velocidadMovimiento, m_Rigidbody2D.velocity.y) * animator.transform.right;
 
        }
        else
        {
            Camina = true;
            m_Rigidbody2D.velocity = new Vector2(0, m_Rigidbody2D.velocity.y-1);
        }

    }


    public void ApplyDam(float damage)
    {
        animator.SetBool("Hit", true);
        float direction = damage / Mathf.Abs(damage);
        damage = Mathf.Abs(damage);
        life -= damage;
        m_Rigidbody2D.velocity = new Vector2(0f, 0f);
        m_Rigidbody2D.AddForce(new Vector2(direction * 300f, 100f));
        if (life <= 0)
        {
            
            Muerte();
        }
    }

    // Método para activar el GameObject
    public void Muerte()
    {
        DropItem();
        animator.SetBool("Death", true); // Activa el bool "isDead" para activar la animación de muerte.
        Destroy(gameObject);
    }

    public void MirarJugador()
    {
        if ((fireWarriorPrefab.position.x > transform.position.x && !mirandoDerecha) || (fireWarriorPrefab.position.x < transform.position.x && mirandoDerecha))
        {
            mirandoDerecha = !mirandoDerecha;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
    }

    public void Ataque()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(ControladorAtaque.position, radioAtaque);
        foreach (Collider2D colision in objetos)
        {
            if (colision.CompareTag("Player"))
            {
                colision.GetComponent<FireWarriorController2D>().ApplyDamage(dañoAtaque, Vector3.zero);
            }
        }
    }
    private void DropItem()
    {
        if (dropPrefab != null)
        {
            // Crear el objeto del item
            Instantiate(dropPrefab, transform.position, Quaternion.identity);

            // Obtener el componente del item
            //ItemPickup itemPickup = item.GetComponent<ItemPickup>();
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ControladorAtaque.position, radioAtaque);
    }
}
