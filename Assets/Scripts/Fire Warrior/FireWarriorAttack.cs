using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireWarriorAttack : MonoBehaviour
{
    private PlayerInputActions playerControls;
    private Rigidbody2D m_Rigidbody2D;

    [Header("Ataque")]
    [SerializeField] private Transform ConAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private float dañoAtaque;
    public Transform Jefe;
    //Config:
    public GameObject throwableObject;
    public Transform attackCheck;
    public Animator animator;
    public GameObject cam;

    [SerializeField] private float dmgValue = 4;
    [SerializeField] private float attackCooldownSecs = 0.25f;

    //States:
    private int attackNumber = 0;
    public bool canAttack = true;
    public bool isTimeToCheck = false;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        playerControls = new PlayerInputActions();
    }

    void Start()
    {
        // Start is called before the first frame update

    }

    private void OnEnable()
    {
        playerControls.Player.Attack.Enable();
    }

    private void OnDisable()
    {
        playerControls.Player.Attack.Disable();
    }

    void FixedUpdate()
    {
        // Update is called once per frame
        if (playerControls.Player.Attack.triggered && canAttack)
        {
            canAttack = false;
            switch (attackNumber)
            {
                case 0: animator.SetBool("IsAttacking0", true); break;
                case 1: animator.SetBool("IsAttacking1", true); break;
                case 2: animator.SetBool("IsAttacking2", true); break;
                default: animator.SetBool("IsAttacking0", true); break;
            }
            attackNumber += 1;
            if (attackNumber % 3 == 0) attackNumber = 0;
            StartCoroutine(AttackCooldown());
            Ataque();
        }

        //playerControls.Player.Shoot.triggered
        if (Input.GetKeyDown(KeyCode.Q) && canAttack)
        {
            canAttack = false;
            GameObject throwableWeapon = Instantiate(throwableObject, transform.position + new Vector3(transform.localScale.x * 0.2f, 0.35f), Quaternion.identity) as GameObject;
            Vector2 direction = new Vector2(transform.localScale.x, 0);
            throwableWeapon.transform.localScale = new Vector3(transform.localScale.x, 1, 1); // Ajustar la escala del objeto del disparo
            throwableWeapon.GetComponent<FireWarriorThrowableWeapon>().direction = direction;
            throwableWeapon.name = "ThrowableWeapon";
            throwableWeapon.AddComponent<DestroyOnInvisible>(); // Agregar el script DestroyOnInvisible al proyectil
            StartCoroutine(AttackCooldown());
        }
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSecondsRealtime(attackCooldownSecs);
        canAttack = true;
    }
    public void Ataque()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(ConAtaque.position, radioAtaque);
        foreach (Collider2D colision in objetos)
        {
            if (colision.CompareTag("JEFE"))
            {
                colision.GetComponent<Jefe>().ApplyDam(dañoAtaque);
            }
        }
    }

    public void DoDashDamage()
    {
        dmgValue = Mathf.Abs(dmgValue);
        Collider2D[] collidersEnemies = Physics2D.OverlapCircleAll(attackCheck.position, 0.9f);
        for (int i = 0; i < collidersEnemies.Length; i++)
        {
            if (collidersEnemies[i].gameObject.tag == "JEFE")
            {
                if (collidersEnemies[i].transform.position.x - transform.position.x < 0)
                {
                    dmgValue = -dmgValue;
                }
                collidersEnemies[i].gameObject.SendMessage("ApplyDamage", dmgValue);
                cam.GetComponent<CameraFollow>().ShakeCamera();
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(ConAtaque.position, radioAtaque);
    }
}
