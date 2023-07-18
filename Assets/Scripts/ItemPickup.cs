using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public int minTimeToAdd = 5; // Tiempo m�nimo a agregar al recoger el item
    public int maxTimeToAdd = 20; // Tiempo m�ximo a agregar al recoger el item

    private float timeToAdd; // Tiempo que se agrega al recoger el item

    private TimeManager timeManager;

    private void Start()
    {
        timeManager = FindObjectOfType<TimeManager>();
    }

private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Choque");
            // Obtener el componente TimeManager en el jugador
            timeManager = other.GetComponent<TimeManager>();

            // Agregar tiempo al jugador
            // Generar un n�mero aleatorio entre minTimeToAdd y maxTimeToAdd
            timeToAdd = Random.Range(minTimeToAdd, maxTimeToAdd);
            timeManager.AddTime(timeToAdd);

            // Destruir el objeto del item
            Debug.Log("Seek and destroy");
            Destroy(this.gameObject);
        }
    }
}
