using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Constructable : MonoBehaviour, IDamagaeble
{

    NavMeshObstacle obstacle;

    private float constHealth;
    public float constMaxHealth;

    public HealthTracker healthTracker;

    public bool isEnemy = false;

    public BuildingType buildingType;
    private void Start()
    {
        constHealth = constMaxHealth;
        healthTracker.gameObject.SetActive(false); // Sembunyikan saat game mulai
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        healthTracker.UpdateSliderValue(constHealth, constMaxHealth);

        if (constHealth >= constMaxHealth)
        {
            healthTracker.gameObject.SetActive(false); // Sembunyikan lagi kalau full
        }

        if (constHealth <= 0)
        {
            ResourceManager.instance.UpdateBuildingChanged(buildingType, false);

            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!healthTracker.gameObject.activeSelf)
        {
            healthTracker.gameObject.SetActive(true); // Munculkan ketika kena damage pertama kali
        }

        constHealth -= damage;
        UpdateHealthUI();
    }

    public void ConstructableWasPlaced()
    {
         ActivateObstacle();
        
        if (isEnemy)
        {
            gameObject.tag = "Enemy";
        }
    }

    private void ActivateObstacle()
    { 
        obstacle = GetComponentInChildren<NavMeshObstacle>();
        obstacle.enabled = true;
    }
}
