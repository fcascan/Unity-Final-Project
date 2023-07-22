using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Walking,
    Jumping,
    Dashing,
    MeleeAttacking,
    RangeAttacking,
    Hitted
}

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float life = 10f;
    [SerializeField] private float maxFollowDistance = 5f;
    [SerializeField] private float meleeAttackDistance = 0.85f;
    [SerializeField] private float rangeAttackDistance = 2f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float dashForce = 15f;
    [SerializeField] private float meleeDamage = 1f;
    [SerializeField] private GameObject throwableObject;
    [SerializeField] private GameObject dropPrefab;       // Prefab del item a soltar

    private Transform _playerTransform;
    private Transform _attackCheck;
    private Animator _animator;
    private Rigidbody2D _rigidBody2D;
    private CapsuleCollider2D capsule;

    private EnemyState currentState = EnemyState.Idle;
    private bool isDead = false;
    private bool isActionInProgress = false;
    private bool isFacingRight = true;

    float distToPlayer = 0f;
    float distToPlayerX = 0f;
    float distToPlayerY = 0f;

    private void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        capsule = GetComponent<CapsuleCollider2D>();
        _attackCheck = transform.Find("AttackCheck").transform;
        _animator = GetComponent<Animator>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (currentState == EnemyState.Hitted || isDead)
            return;

        if (life <= 0f)
        {
            Kill();
        }

        distToPlayer = Vector2.Distance(transform.position, _playerTransform.position);
        distToPlayerX = _playerTransform.position.x - transform.position.x;
        distToPlayerY = _playerTransform.position.y - transform.position.y;

        if (distToPlayer <= meleeAttackDistance)
        {
            if (!isActionInProgress)
            {
                MeleeAttack();
            }
            MoveTowardsPlayer(0.5f);
        }
        else if (currentState != EnemyState.MeleeAttacking && currentState != EnemyState.RangeAttacking && currentState != EnemyState.Dashing && !_animator.GetBool("IsJumping") && distToPlayer <= maxFollowDistance)
        {
            // Cuando no está realizando una acción aleatoria o un ataque melee, camina hacia el jugador.
            Walk();
        }
        if (distToPlayer <= maxFollowDistance && !isActionInProgress)
        {
            PerformRandomAction();
        }
        else
        {
            currentState = EnemyState.Idle;
        }

        //Correcciones orientacion:
        if (_playerTransform.position.x > transform.position.x && !isFacingRight)
            Flip();
        else if (_playerTransform.position.x < transform.position.x && isFacingRight)
            Flip();
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void MeleeAttack()
    {
        isActionInProgress = true;
        currentState = EnemyState.MeleeAttacking;
        _animator.SetBool("MeleeAttack", true);
        StartCoroutine(WaitForAnimation("MeleeAttack"));
        Collider2D[] collidersEnemies = Physics2D.OverlapCircleAll(_attackCheck.position, 0.3f);
        for (int i = 0; i < collidersEnemies.Length; i++)
        {
            if (collidersEnemies[i].gameObject.tag == "Player")
            {
                collidersEnemies[i].gameObject.GetComponent<FireWarriorController2D>().ApplyDamage(meleeDamage, transform.position);
            }
        }
        StartCoroutine(EndCurrentAction());
    }

    //private void RangeAttack()
    //{
    //    Debug.Log("RangeAttack");
    //    _animator.SetBool("RangeAttack", true);
    //    Vector2 direction = _playerTransform.position - transform.position;
    //    direction.Normalize();

    //    GameObject throwableProj = Instantiate(throwableObject, transform.position + (Vector3)direction, Quaternion.identity);
    //    throwableProj.GetComponent<ThrowableProjectile>().owner = gameObject;
    //    throwableProj.GetComponent<ThrowableProjectile>().direction = direction * 0.5f;
    //    StartCoroutine(WaitForAnimation("RangeAttack"));
    //}

    private void RangeAttack()
    {
        Debug.Log("RangeAttack");
        _animator.SetBool("RangeAttack", true);

        // Ajustar la velocidad del proyectil
        float projectileSpeed = 10f; // Ajusta este valor según la velocidad deseada del proyectil

        Vector2 direction = _playerTransform.position - transform.position;
        direction.Normalize();

        GameObject throwableProj = Instantiate(throwableObject, transform.position + (Vector3)direction, Quaternion.identity);
        throwableProj.GetComponent<ThrowableProjectile>().owner = gameObject;
        throwableProj.GetComponent<ThrowableProjectile>().direction = direction * 0.5f;
        throwableProj.GetComponent<ThrowableProjectile>().speed = projectileSpeed; // Configura la velocidad

        StartCoroutine(WaitForAnimation("RangeAttack"));
    }


    private void Dash()
    {
        Debug.Log("Dash");
        _animator.SetBool("IsDashing", true);
        //_rigidBody2D.velocity += new Vector2(distToPlayerX / Mathf.Abs(distToPlayerX) * dashForce, 0f);
        float dashDirection = Mathf.Sign(distToPlayerX);
        _rigidBody2D.AddForce(new Vector2(dashDirection * dashForce * 15f, 0f), ForceMode2D.Impulse);
        StartCoroutine(WaitForAnimation("IsDashing"));
    }

    private void Walk()
    {
        _animator.SetBool("IsWalking", true);
        MoveTowardsPlayer(1);
        StartCoroutine(WaitForAnimation("IsWalking"));
    }

    private void Jump()
    {
        Debug.Log("Jump");
        _animator.SetBool("IsJumping", true);
        Vector3 targetVelocity = new Vector2(distToPlayerX / Mathf.Abs(distToPlayerX) * speed, _rigidBody2D.velocity.y);
        Vector3 velocity = Vector3.zero;
        _rigidBody2D.velocity = Vector3.SmoothDamp(_rigidBody2D.velocity, targetVelocity, ref velocity, 1.05f);
        //_rigidBody2D.gravityScale = 3f;
        _rigidBody2D.AddForce(new Vector2(0f, 850f));
        StartCoroutine(WaitForAnimation("IsJumping"));
    }

    private void Idle()
    {
        _animator.SetBool("IsIdle", true);
        StartCoroutine(WaitForAnimation("IsIdle"));
    }

    private void MoveTowardsPlayer(float percent)
    {
        Vector2 direction = new Vector2(_playerTransform.position.x - transform.position.x, 0);
        direction.Normalize();
        _rigidBody2D.velocity = direction * speed * percent;
    }

    private void ResetAnimatorVariables()
    {
        _animator.SetBool("IsWalking", false);
        _animator.SetBool("IsIdle", false);
        _animator.SetBool("IsJumping", false);
        _animator.SetBool("IsDashing", false);
        _animator.SetBool("MeleeAttack", false);
        _animator.SetBool("RangeAttack", false);
    }

    private void PerformRandomAction()
    {
        isActionInProgress = true;

        float randomValue = Random.Range(0f, 1f);

        if (randomValue <= 0.5f)
        {
            Walk();
        }
        else if (randomValue > 0.5f && randomValue <= 0.65f)
        {
            Jump();
        }
        else if (randomValue > 0.65f && randomValue <= 0.75f)
        {
            Dash();
        }
        else if (randomValue > 0.75f && randomValue <= 0.95f)
        {
            RangeAttack();
        }
        else
        {
            Idle();
        }

        StartCoroutine(EndCurrentAction());
    }

    private IEnumerator EndCurrentAction()
    {
        yield return new WaitForSeconds(0.5f); // Espacio osociso entre acciones
        ResetAnimatorVariables();
        currentState = EnemyState.Idle;
        isActionInProgress = false;
    }

    private IEnumerator WaitForAnimation(string animationName)
    {
        yield return new WaitForSeconds(0.025f);
        while (_animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
        {
            yield return null;
        }
        _animator.SetBool(animationName, false);
    }

    public void ApplyDamage(float damage)
    {
        if (currentState == EnemyState.Hitted) return;

        life -= Mathf.Abs(damage);
        Debug.Log("Enemy Hitted. Life: " + life);

        StartCoroutine(OnHit());
        _rigidBody2D.velocity = Vector2.zero;
        _rigidBody2D.AddForce(new Vector2(damage, 3f), ForceMode2D.Impulse);
    }

    public void Kill()
    {
        StopCoroutine("WaitForAnimation");
        StopCoroutine("EndCurrentAction");
        isDead = true;
        ResetAnimatorVariables();
        MoveTowardsPlayer(0);
        isActionInProgress = true;
        DestroyEnemy();
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

    private IEnumerator OnHit()
    {
        currentState = EnemyState.Hitted;
        _animator.SetBool("Hitted", true);
        yield return new WaitForSeconds(0.3f);
        _animator.SetBool("Hitted", false);
        currentState = EnemyState.Idle;
    }

    private void DestroyEnemy()
    {
        capsule.size = new Vector2(0.01f, 0.01f);
        capsule.offset = new Vector2(0f, -0.25f);
        _animator.SetBool("IsDead", true);
        DropItem();
        Destroy(gameObject, 5f);
    }
}
