using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarJefe : MonoBehaviour
{
    [Header("Activar Jefe")]
    [SerializeField] private Transform ControladorActivar;
    [SerializeField] private Vector2 dimensionesCaja;
    private bool Check = false;
    public GameObject objetoParaActivarDesactivar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Check == false)
        {
            Collider2D[] odjetos = Physics2D.OverlapBoxAll(ControladorActivar.position, dimensionesCaja, 0f);

            foreach (Collider2D colisiones in odjetos)
            {
                if (colisiones.CompareTag("Player"))
                {
                    objetoParaActivarDesactivar.SetActive(true);
                    Check = true;
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(ControladorActivar.position, dimensionesCaja);
    }
}
