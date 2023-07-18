using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float life = 10;
	private bool isPlat;
	private bool isObstacle;
	private Transform fallCheck;
	private Transform wallCheck;
	public LayerMask turnLayerMask;
	private Rigidbody2D rb;

	private bool facingRight = true;
	
	public float speed = 5f;
    public float rangeDist = 5f;
    public float meleeDist = 1.5f;

    public bool isInvincible = false;
	private bool isHitted = false;


    public GameObject player;

    void Awake()
    {
        fallCheck = transform.Find("FallCheck");
        wallCheck = transform.Find("WallCheck");
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (life <= 0)
        {
            transform.GetComponent<Animator>().SetBool("IsDead", true);
            StartCoroutine(DestroyEnemy());
            return; // Salir del método si el enemigo está muerto
        }

        isPlat = Physics2D.OverlapCircle(fallCheck.position, .2f, 1 << LayerMask.NameToLayer("Default"));
        isObstacle = Physics2D.OverlapCircle(wallCheck.position, .2f, turnLayerMask);

        if (!isHitted && life > 0 && Mathf.Abs(rb.velocity.y) < 0.5f)
        {
            if (player != null)
            {
                float distToPlayer = player.transform.position.x - transform.position.x;

                // Verificar la distancia al jugador para decidir el comportamiento
                if (Mathf.Abs(distToPlayer) < rangeDist)
                {
                    // Dentro del rango de persecución
                    if (Mathf.Abs(distToPlayer) > meleeDist)
                    {
                        // Moverse hacia el jugador
                        if (facingRight)
                        {
                            rb.velocity = new Vector2(speed, rb.velocity.y);
                        }
                        else
                        {
                            rb.velocity = new Vector2(-speed, rb.velocity.y);
                        }
                    }
                    else
                    {
                        // Dentro del rango de ataque cuerpo a cuerpo
                        rb.velocity = new Vector2(0f, rb.velocity.y);
                        // Agregar lógica de ataque aquí
                    }
                }
                else
                {
                    // Fuera del rango de persecución, realizar idle
                    rb.velocity = new Vector2(0f, rb.velocity.y);
                    // Agregar lógica de idle aquí
                }

                // Cambiar la dirección del enemigo según la posición del jugador
                if (distToPlayer > 0f && !facingRight)
                {
                    Flip();
                }
                else if (distToPlayer < 0f && facingRight)
                {
                    Flip();
                }
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    public void ApplyDamage(float damage) {
		if (!isInvincible) 
		{
			float direction = damage / Mathf.Abs(damage);
			damage = Mathf.Abs(damage);
			transform.GetComponent<Animator>().SetBool("Hit", true);
			life -= damage;
			rb.velocity = Vector2.zero;
			rb.AddForce(new Vector2(direction * 500f, 100f));
			StartCoroutine(HitTime());
		}
	}

	void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player" && life > 0)
		{
			collision.gameObject.GetComponent<FireWarriorController2D>().ApplyDamage(2f, transform.position);
		}
	}

	IEnumerator HitTime()
	{
		isHitted = true;
		isInvincible = true;
		yield return new WaitForSeconds(0.1f);
		isHitted = false;
		isInvincible = false;
	}

	IEnumerator DestroyEnemy()
	{
		CapsuleCollider2D capsule = GetComponent<CapsuleCollider2D>();
		capsule.size = new Vector2(1f, 0.25f);
		capsule.offset = new Vector2(0f, -0.8f);
		capsule.direction = CapsuleDirection2D.Horizontal;
		yield return new WaitForSeconds(0.25f);
		rb.velocity = new Vector2(0, rb.velocity.y);
		yield return new WaitForSeconds(3f);
		Destroy(gameObject);
	}
}
