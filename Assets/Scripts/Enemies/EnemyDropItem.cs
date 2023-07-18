using UnityEngine;

public class EnemyDropItem : MonoBehaviour
{
    public GameObject itemPrefab; // Prefabricado del item a soltar

    private void OnDestroy()
    {
        // Crear el objeto del item
        GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity);

        // Obtener el componente del item
        ItemPickup itemPickup = item.GetComponent<ItemPickup>();
    }
}
