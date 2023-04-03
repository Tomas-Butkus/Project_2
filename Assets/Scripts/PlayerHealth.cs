using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public SharedFloat health;
    public SharedFloat maxHealth;
    [SerializeField] private Slider heatlhSlider;
    [SerializeField] private float damageReceived; //gamage received over time
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Timer timer;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    public void CheckIfDead()
    {
        if(health.Amount > maxHealth.Amount)
        {
            health.Amount = maxHealth.Amount;
        }
        if(health.Amount <= 0f)
        {
            Dead();
        }
    }

    public void Damage(float damage)
    {
        if(!_playerController.dashing)
            health.Amount -= damage;
        CheckIfDead();
    }

    public void Kill(bool ignoreDashing)
    {
        if (!ignoreDashing && !_playerController.dashing)
        {
            Dead();
        }
        
    }

    private void Start()
    {
        maxHealth.Amount = 100f;
        health.Amount = maxHealth.Amount;
        heatlhSlider.maxValue = maxHealth.Amount;
    }

    private void Update()
    {
        //DamageOverTime();
        CheckIfDead();
    }

    private void Dead()
    {
        gameObject.SetActive(false);
        heatlhSlider.gameObject.SetActive(false);
        StateManager.GameOver();
        timer.enabled = false;
    }

    private void DamageOverTime()
    {
        health.Amount -= damageReceived * Time.deltaTime;
    }

    private void OnGUI()
    {
        float t = Time.deltaTime / 1f;
        heatlhSlider.value = Mathf.Lerp(heatlhSlider.value, health.Amount, t);
    }
}
