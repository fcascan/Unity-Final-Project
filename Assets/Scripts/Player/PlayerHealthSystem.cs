using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour {
    public event Action<float> Damaged = delegate { };
    public event Action<float> Healed = delegate { };
    public event Action Killed = delegate { };

    //Config:
    [SerializeField] private float _startingHealth = 100;
    [SerializeField] private float _maxHealth = 100;
    
    //States:
    private float _currentHealth;


    /* Callbacks */
    void Start() {
        // Start is called before the first frame update


    }

    void Update() {
        // Update is called once per frame


    }

    private void Awake() {
        CurrentHealth = StartingHealth;
    }


    /* Public Methods */
    public void Heal(float ammount) {
        CurrentHealth += ammount;
        Healed?.Invoke(ammount);
    }
    public void TakeDamage(float ammount) {
        CurrentHealth -= ammount;
        Damaged?.Invoke(ammount);
        if (CurrentHealth <= 0) Kill();
    }
    public void Kill() { 
        Killed?.Invoke();
        gameObject.SetActive(false);
    }


    /* Getters & Setters */
    public float StartingHealth {
        get { return _startingHealth; }
    }
    public float MaxHealth {
        get { return _maxHealth; }
    }
    public float CurrentHealth {
        get { return _currentHealth; }
        set { if (value > _maxHealth) value = _maxHealth; else _currentHealth = value; }
    }
}
