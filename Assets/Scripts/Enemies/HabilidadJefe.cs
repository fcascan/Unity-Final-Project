using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilidadJefe : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float daño;
    [SerializeField] private Vector2 dimensionesCaja;
    [SerializeField] private Transform posicionCaja;
    [SerializeField] private float tiempoDeVida;
    public Transform fireWarriorPrefab;
    // Start is called before the first frame update

    void Start()
    {

        Destroy(gameObject, tiempoDeVida);

    }

    public void Golpe()
    {
        Collider2D[] odjetos = Physics2D.OverlapBoxAll(posicionCaja.position, dimensionesCaja, 0f);

        foreach (Collider2D colisiones in odjetos)
        {
            if(colisiones.CompareTag("Player"))
            {
                colisiones.GetComponent<FireWarriorController2D>().ApplyDamage(daño,Vector3.zero);
            }
        }
    }
    // Update is called once per frame
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(posicionCaja.position, dimensionesCaja);
    }
}
