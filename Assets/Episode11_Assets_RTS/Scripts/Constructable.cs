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

    public Vector3 builPosition;


    public bool inPreviewMode;

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
            ResourceManager.instance.UpdateBuildingChanged(buildingType, false, builPosition);

            SoundManager.instance.PlayBuildingDestructionSound();

            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (inPreviewMode == false)
        {
            if (constHealth > 0 && builPosition != Vector3.zero)
            {
                ResourceManager.instance.SellingBuilding(buildingType);
            }
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
        Debug.Log("placed!");

        ActivateObstacle();
        
        if (isEnemy)
        {
            gameObject.tag = "Enemy";
        }

        if (GetComponent<PowerUser>() != null)
        {
            GetComponent<PowerUser>().PowerOn();
        }
    }

    private void ActivateObstacle()
    { 
        obstacle = GetComponentInChildren<NavMeshObstacle>();
        obstacle.enabled = true;
    }
}
