using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public float startingTime = 60f; // Tiempo inicial en segundos
    public float minTimeToAdd = 5f; // Tiempo mínimo para agregar al matar un enemigo
    public float maxTimeToAdd = 20f; // Tiempo máximo para agregar al matar un enemigo
    public Text timeText; // Referencia al componente Text para mostrar el tiempo restante

    private float currentTime; // Tiempo actual restante

    private void Start()
    {
        // Inicializar el tiempo actual desde PlayerPrefs o el valor predeterminado
        currentTime = PlayerPrefs.GetFloat("Time", startingTime);
        UpdateTimeText();
    }

    private void Update()
    {
        // Reducir el tiempo restante
        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            // Tiempo agotado, realizar acciones necesarias (por ejemplo, game over)
            currentTime = 0f;
        }

        UpdateTimeText();
    }

    public void AddTime(float timeToAdd)
    {
        currentTime += timeToAdd;
    }
    private void UpdateTimeText()
    {
        if (timeText != null)
        {
            timeText.text = Mathf.Round(currentTime).ToString(); // Actualizar el texto del tiempo restante
        }
    }

    public void AddTimeOnEnemyKill()
    {
        float timeToAdd = Random.Range(minTimeToAdd, maxTimeToAdd);
        currentTime += timeToAdd;
        UpdateTimeText();
    }

    private void OnDestroy()
    {
        // Guardar el tiempo actual en PlayerPrefs para que persista en las siguientes escenas
        PlayerPrefs.SetFloat("Time", currentTime);
    }
}
