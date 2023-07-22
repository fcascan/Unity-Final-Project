using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWarriorThrowableWeapon : MonoBehaviour
{
	public Vector2 direction;
	public bool hasHit = false;
	public float speed = 10f;
	[Header("Ataque")]
	[SerializeField] private Transform ConiAtaque;
	[SerializeField] private float radioAtaque;
	[SerializeField] private float dañoAtaque;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if ( !hasHit)
		GetComponent<Rigidbody2D>().velocity = direction * speed;
		Ataque();
	}
	public void Ataque()
	{
		Collider2D[] objetos = Physics2D.OverlapCircleAll(ConiAtaque.position, radioAtaque);
		foreach (Collider2D colision in objetos)
		{
			if (colision.CompareTag("JEFE"))
			{
				colision.GetComponent<Jefe>().ApplyDam(dañoAtaque);
			}
		}
	}
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(ConiAtaque.position, radioAtaque);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Enemy")
		{
			collision.gameObject.SendMessage("ApplyDamage", Mathf.Sign(direction.x) * 2f);
			Destroy(gameObject);
		}
		else if (collision.gameObject.tag != "Player")
		{
			Destroy(gameObject);
		}
	}
}
