using System;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public event Action<float> TimeVida;
    public event Action<float> TimeMas; // Evento que se dispara cuando se agrega tiempo
    public event Action<float> Timedown; // Evento que se dispara cuando se agrega tiempo
    public event Action<float> TimeAdded; // Evento que se dispara cuando se agrega tiempo
    public event Action TimeReset; // Evento que se dispara cuando el tiempo se resetea a 60
    private float timeLife; // Tiempo que se agrega al recoger el item
    public float startingTime; // Tiempo inicial en segundos
    private TextMeshProUGUI timeText; // Referencia al componente Text para mostrar el tiempo restante

    private float currentTime; // Tiempo actual restante
    private float timedown;
    private void Start()
    {
        // Inicializar el tiempo actual desde PlayerPrefs o el valor predeterminado
        currentTime = PlayerPrefs.GetFloat("Time", startingTime);
        if (currentTime != startingTime)
        {
            currentTime = startingTime;
        }
        timeText = GetComponent<TextMeshProUGUI>();
        UpdateTimeText();
    }

    private void FixedUpdate()
    {
        // Reducir el tiempo restante
        currentTime -= Time.deltaTime;
        FireWarriorController2D timeManager = FindObjectOfType<FireWarriorController2D>();
        timedown = currentTime;
        timeManager.VidaTiempo(timedown);
        TimeVida?.Invoke(timedown);

        if (currentTime <= 0f)
        {
            // Tiempo agotado, realizar acciones necesarias (por ejemplo, game over)
            currentTime = 0f;
            FireWarriorController2D controller = GameObject.FindGameObjectWithTag("Player").GetComponent<FireWarriorController2D>();
            if (controller != null)
            {
                // Llamar al método Kill del componente FireWarriorController2D
                controller.Kill();

                // Resetear el tiempo a 60
                ResetTime();
            }
        }

        UpdateTimeText();
    }

    public void TimeIne(float timeToAdd)
    {
        startingTime += timeToAdd;

        // Disparar el evento TimeAdded con el valor de tiempo agregado
        TimeMas?.Invoke(timeToAdd);
    }
    public void downTime(float timeToAdd)
    {
        currentTime -= timeToAdd;

        // Disparar el evento TimeAdded con el valor de tiempo agregado
        Timedown?.Invoke(timeToAdd);
    }
    public void AddTime(float timeToAdd)
    {
        currentTime += timeToAdd;

        // Disparar el evento TimeAdded con el valor de tiempo agregado
        TimeAdded?.Invoke(timeToAdd);
    }

    public void ResetTime()
    {
        currentTime = startingTime;

        // Disparar el evento TimeReset
        TimeReset?.Invoke();
    }

    private void UpdateTimeText()
    {
        if (timeText != null)
        {
            timeText.text = Mathf.Round(currentTime).ToString("0"); // Actualizar el texto del tiempo restante
        }
    }

    private void OnDestroy()
    {
        // Guardar el tiempo actual en PlayerPrefs para que persista en las siguientes escenas
        PlayerPrefs.SetFloat("Time", currentTime);
    }
}
