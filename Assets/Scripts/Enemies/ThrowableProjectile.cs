using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableProjectile : MonoBehaviour
{
	public Vector2 direction;
	public bool hasHit = false;
	[SerializeField] public float speed = 15f;
	[SerializeField] public float daño = 2f;
	[SerializeField] public float radioAtaque;
	public GameObject owner;
	[SerializeField] private Transform ConAtaque;
	// Update is called once per frame
	void FixedUpdate()
    {
		if ( !hasHit)
		GetComponent<Rigidbody2D>().velocity = direction * speed;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		Collider2D[] odjetos = Physics2D.OverlapCircleAll(ConAtaque.position, radioAtaque);
		foreach (Collider2D colisiones in odjetos)
		{
			if (colisiones.CompareTag("Player"))
			{
				colisiones.GetComponent<FireWarriorController2D>().ApplyDamage(daño, transform.position);
				Destroy(gameObject);
			}
		}
	}
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(ConAtaque.position, radioAtaque);
	}
}
