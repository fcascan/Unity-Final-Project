using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public int minTimeToAdd = 5; // Tiempo mínimo a agregar al recoger el item
    public int maxTimeToAdd = 20; // Tiempo máximo a agregar al recoger el item

    private float timeToAdd; // Tiempo que se agrega al recoger el item

    private GameObject tiempo;
    private TimeManager timeManager;
    private void Start()
    {
        tiempo = GameObject.FindGameObjectWithTag("Tiempo");
        if (tiempo != null)
        {
            // Suscribirse al evento TimeAdded del TimeManager
            timeManager = tiempo.GetComponent<TimeManager>;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Choque");
            // Obtener el componente TimeManager en el jugador
            timeManager = other.GetComponent<TimeManager>();

            // Agregar tiempo al jugador
            // Generar un número aleatorio entre minTimeToAdd y maxTimeToAdd
            timeToAdd = Random.Range(minTimeToAdd, maxTimeToAdd);
            timeManager.AddTime(timeToAdd);

            // Destruir el objeto del item
            Debug.Log("Seek and destroy");
            Destroy(this.gameObject);
        }
    }

    private void HandleTimeAdded(float timeAdded)
    {
        // Aquí puedes realizar cualquier acción necesaria al agregar tiempo al jugador
        Debug.Log("Time added to player: " + timeAdded);

        // Resto del código aquí
    }
}
