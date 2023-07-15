using UnityEngine;

public class EnemyDropItem : MonoBehaviour
{
    public GameObject itemPrefab; // Prefabricado del item a soltar
    public float minTimeToAdd = 5f; // Tiempo mínimo a agregar al recoger el item
    public float maxTimeToAdd = 20f; // Tiempo máximo a agregar al recoger el item

    private void OnDestroy()
    {
        // Generar un número aleatorio entre minTimeToAdd y maxTimeToAdd
        float timeToAdd = Random.Range(minTimeToAdd, maxTimeToAdd);

        // Crear el objeto del item
        GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity);

        // Obtener el componente del item
        ItemPickup itemPickup = item.GetComponent<ItemPickup>();

        if (itemPickup != null)
        {
            // Establecer el tiempo a agregar en el componente del item
            itemPickup.timeToAdd = timeToAdd;
        }
    }
}
