using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public float timeToAdd = 10f; // Tiempo que se agrega al recoger el item

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Obtener el componente TimeManager en el jugador
            TimeManager timeManager = other.GetComponent<TimeManager>();

            if (timeManager != null)
            {
                // Agregar tiempo al jugador
                timeManager.AddTime(timeToAdd);

                // Destruir el objeto del item
                Destroy(gameObject);
            }
        }
    }
}
