using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jefe : MonoBehaviour
{
    private Animator animator;
    public Rigidbody2D m_Rigidbody2D;
    public Transform fireWarriorPrefab;
    private bool mirandoDerecha = true;
    [Header("vida")]
    [SerializeField] private float life;
    [Header("Ataque")]
    [SerializeField] private Transform ControladorAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private float dañoAtaque;

    [Header("Sonido de Victoria")]
    public AudioClip victorySoundClip;

    // Referencia al AudioManager para reproducir el sonido de victoria
    private AudioManager audioManager;

    // Referencia al script que controla el cartel de "victoria"
    public VictoryPanelController victoryPanel;

    // Variable para controlar si el jefe ya ha muerto
    private bool isDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        fireWarriorPrefab = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        audioManager = AudioManager.Instance; // Obtener la instancia del AudioManager
    }

    void Update()
    {
        float distanciaJugador = Vector2.Distance(transform.position, fireWarriorPrefab.position);
        animator.SetFloat("distanciaJugador", distanciaJugador);
    }

    public void ApplyDam(float damage)
    {

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

    IEnumerator LoadMainMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Cargar el menú principal (Main Menu)
        SceneManager.LoadScene("MainMenu");
    }

    public void Muerte()
    {
        if (isDead) return; // Si el jefe ya ha muerto, no hace nada

        animator.SetBool("Death", true); // Activa el bool "isDead" para activar la animación de muerte.
        isDead = true;

        // Reproducir el sonido de victoria directamente con AudioSource
        if (victorySoundClip != null)
        {
            AudioSource.PlayClipAtPoint(victorySoundClip, transform.position);
        }
        Destroy(gameObject);
        // Mostrar el cartel de "victoria" durante 20 segundos
        victoryPanel.ShowVictoryPanel(20f);

        // Después de 20 segundos, cargar el menú principal
        StartCoroutine(LoadMainMenuAfterDelay(20f));
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ControladorAtaque.position, radioAtaque);
    }
}
