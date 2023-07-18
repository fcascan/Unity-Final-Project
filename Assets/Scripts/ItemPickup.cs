using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemPickup : MonoBehaviour
{
    public int minTimeToAdd = 5; // Tiempo m�nimo a agregar al recoger el item
    public int maxTimeToAdd = 20; // Tiempo m�ximo a agregar al recoger el item

    private float timeToAdd; // Tiempo que se agrega al recoger el item

    public event Action<float> TimePickedUp; // Evento que se dispara cuando se recoje el �tem

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TimeManager timeManager = FindObjectOfType<TimeManager>();
            if (timeManager != null)
            {
                timeToAdd = Random.Range(minTimeToAdd, maxTimeToAdd);
                timeManager.AddTime(timeToAdd);
                TimePickedUp?.Invoke(timeToAdd);
                Destroy(gameObject);
            }
        }
    }
}
