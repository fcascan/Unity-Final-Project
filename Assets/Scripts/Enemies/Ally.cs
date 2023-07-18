using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : MonoBehaviour
{
    [SerializeField] private float life = 15f;
    [SerializeField] private float maxFollowDistance = 5f; // Distancia máxima a la cual el enemigo se acerca al jugador
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool isInvincible = false;
    [SerializeField] private float dashForce = 5f;
    [SerializeField] private GameObject enemy;
    [SerializeField] private float meleeDist = 0.85f;
    [SerializeField] private float rangeDist = 2f;
    [SerializeField] private GameObject throwableObject;
    [SerializeField] private float damageValue = 1f;

    private bool facingRight = true;
    private bool isHitted = false;
    private bool isDashing = false;

    private float distToPlayerX;
    private float distToPlayerY;
    private float distToPlayer;

    private bool canAttack = true;
    private float randomDecision = 0f;
    private bool doOnceDecision = true;
    private bool endDecision = false;
    private float nextDecisionTimeFast = 0.3f;
    private float nextDecisionTimeSlow = 0.6f;

    private Rigidbody2D m_Rigidbody2D;
    private Transform attackCheck;
    private Animator animator;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        attackCheck = transform.Find("AttackCheck").transform;
        animator = GetComponent<Animator>();

        if (enemy == null)
        {
            enemy = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void FixedUpdate()
    {
        if (life <= 0f) {
            Kill();
        }
        else {
            if (isHitted) {
                if ((distToPlayerX > 0f && facingRight) || (distToPlayerX < 0f && !facingRight)) {
                    Flip();
                }
                GetHitted();
            } else {
                Vector2 enemyPosition = new Vector2(enemy.transform.position.x, enemy.transform.position.y);
                distToPlayerX = enemyPosition.x - transform.position.x;
                distToPlayerY = enemyPosition.y - transform.position.y;
                float distToPlayer = Mathf.Sqrt(Mathf.Pow(distToPlayerX, 2) + Mathf.Pow(distToPlayerY, 2));
                //Debug.Log("distToPlayer: (" + distToPlayer + ")");

                if (distToPlayer > maxFollowDistance) {
                    //Player fuera de alcance
                    m_Rigidbody2D.velocity = new Vector2(0f, m_Rigidbody2D.velocity.y);
                    animator.SetBool("IsWaiting", true);
                }
                else if (distToPlayer <= meleeDist) {
                    //En rango de ataque Melee
                    if ((distToPlayerX > 0f && !facingRight) || (distToPlayerX < 0f && facingRight)) {
                        Flip();
                    }
                    if (canAttack) {
                        MeleeAttack();
                    }
                    else {
                        animator.SetBool("IsWalking", true);
                        m_Rigidbody2D.velocity = new Vector2(distToPlayerX / Mathf.Abs(2 * distToPlayerX) * speed, m_Rigidbody2D.velocity.y);
                    }
                }
                else {
                    if (!endDecision) {
                        if ((distToPlayerX > 0f && !facingRight) || (distToPlayerX < 0f && facingRight))
                            Flip();
                        if (randomDecision < 0.5f)
                            Run();
                        else if (randomDecision >= 0.5f && randomDecision < 0.65f)
                            Jump();
                        else if (randomDecision >= 0.65f && randomDecision < 0.75f)
                            Dash();
                        else if (randomDecision >= 0.75f && randomDecision < 0.95f)
                            RangeAttack();
                        else
                            Idle();
                    }
                    else {
                        endDecision = false;
                    }
                }
            }
            if (m_Rigidbody2D.velocity.x > 0 && !facingRight && life > 0) {
                Flip();
            }
            else if (m_Rigidbody2D.velocity.x < 0 && facingRight && life > 0) {
                Flip();
            }
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public void ApplyDamage(float damage)
    {
        if (!isInvincible)
        {
            animator.SetBool("Hit", true);
            float direction = damage / Mathf.Abs(damage);
            damage = Mathf.Abs(damage);
            life -= damage;
            m_Rigidbody2D.velocity = new Vector2(0f, 0f);
            m_Rigidbody2D.AddForce(new Vector2(direction * 300f, 100f));
            StartCoroutine(HitTime());
        }
    }

    public void GetHitted()
    {
        //TODO
        //ApplyDamage(0f);
    }

    public void MeleeAttack()
    {
        animator.SetBool("Attack", true);
        Collider2D[] collidersEnemies = Physics2D.OverlapCircleAll(attackCheck.position, 0.3f);
        for (int i = 0; i < collidersEnemies.Length; i++)
        {
            if (collidersEnemies[i].gameObject.tag == "Enemy" && collidersEnemies[i].gameObject != gameObject)
            {
                if (transform.localScale.x < 1)
                {
                    damageValue = -damageValue;
                }
                collidersEnemies[i].gameObject.SendMessage("ApplyDamage", damageValue);
            }
            else if (collidersEnemies[i].gameObject.tag == "Player")
            {
                collidersEnemies[i].gameObject.GetComponent<FireWarriorController2D>().ApplyDamage(2f, transform.position);
            }
        }
        StartCoroutine(WaitToAttack(nextDecisionTimeFast));
    }

    public void RangeAttack()
    {
        animator.SetBool("RangeAttack", true);
        if (doOnceDecision)
        {            
            GameObject throwableProj = Instantiate(throwableObject, transform.position + new Vector3(transform.localScale.x * 0.5f, -0.2f), Quaternion.identity) as GameObject;
            throwableProj.GetComponent<ThrowableProjectile>().owner = gameObject;
            Vector2 direction = new Vector2(transform.localScale.x, 0f);
            throwableProj.GetComponent<ThrowableProjectile>().direction = direction;
            StartCoroutine(NextDecision(nextDecisionTimeFast));
        }
    }

    public void Run()
    {
        animator.SetBool("IsWalking", true);
        m_Rigidbody2D.velocity = new Vector2(distToPlayerX / Mathf.Abs(distToPlayerX) * speed, m_Rigidbody2D.velocity.y);
        if (doOnceDecision)
        {
            StartCoroutine(NextDecision(nextDecisionTimeFast));
        }
    }

    public void Jump()
    {
        animator.SetBool("IsWalking", true);
        Vector3 targetVelocity = new Vector2(distToPlayerX / Mathf.Abs(distToPlayerX) * speed, m_Rigidbody2D.velocity.y);
        Vector3 velocity = Vector3.zero;
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, 0.05f);
        if (doOnceDecision)
        {
            m_Rigidbody2D.AddForce(new Vector2(0f, 850f));
            StartCoroutine(NextDecision(nextDecisionTimeSlow));
        }
    }

    public void Dash() 
    {
        animator.SetBool("IsDashing", true);
        m_Rigidbody2D.velocity = new Vector2(distToPlayerX / Mathf.Abs(distToPlayerX) * dashForce, 0f);
        if (doOnceDecision)
        {           
            StartCoroutine(NextDecision(0.1f));
        }
    }

    public void Idle()
    {
        animator.SetBool("IsWaiting", true);
        m_Rigidbody2D.velocity = new Vector2(0f, m_Rigidbody2D.velocity.y);
        if (doOnceDecision)
        {            
            StartCoroutine(NextDecision(nextDecisionTimeSlow));
        }
    }

    public void EndDecision()
    {
        randomDecision = Random.Range(0f, 1f);
        endDecision = true;
    }

    private IEnumerator HitTime()
    {
        isInvincible = true;
        isHitted = true;
        yield return new WaitForSeconds(0.1f);
        isHitted = false;
        isInvincible = false;
        clearAnimatorVariables();
    }

    private IEnumerator WaitToAttack(float time)
    {
        canAttack = false;
        yield return new WaitForSeconds(time);
        canAttack = true;
        clearAnimatorVariables();
    }

    private IEnumerator NextDecision(float time)
    {
        doOnceDecision = false;
        yield return new WaitForSeconds(time);
        EndDecision();
        doOnceDecision = true;
        clearAnimatorVariables();
    }

    private IEnumerator DestroyEnemy()
    {
        CapsuleCollider2D capsule = GetComponent<CapsuleCollider2D>();
        capsule.size = new Vector2(1f, 0.25f);
        capsule.offset = new Vector2(0f, -0.8f);
        capsule.direction = CapsuleDirection2D.Horizontal;
        animator.SetBool("IsDead", true);
        yield return new WaitForSeconds(0.5f);
        m_Rigidbody2D.velocity = new Vector2(0f, m_Rigidbody2D.velocity.y);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void clearAnimatorVariables()
    {
        animator.SetBool("Hit", false);
        animator.SetBool("IsDead", false);
        animator.SetBool("Attack", false);
        animator.SetBool("IsWaiting", false);
        animator.SetBool("IsDashing", false);
        animator.SetBool("RangeAttack", false);
        animator.SetBool("IsWalking", false);
    }

    public void Kill ()
    {
        StartCoroutine(DestroyEnemy());
    }
}
