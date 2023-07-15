using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance; // Referencia estática al objeto TimeManager

    private float currentTime; // Tiempo actual

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddTime(float timeToAdd)
    {
        currentTime += timeToAdd;
    }
}
