using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private float unitHealth;
    public float unitMaxHealth;

    public HealthTracker healthTracker;

    
    void Start()
    {
        UnitSelectionManager.instance.allUnitsList.Add(gameObject);

        unitHealth = unitMaxHealth;
        UpdateHealthUI();
    }

    private void OnDestroy()
    {
        UnitSelectionManager.instance.allUnitsList.Remove(gameObject);
    }

    private void UpdateHealthUI()
    {
        healthTracker.UpdateSliderValue(unitHealth, unitMaxHealth);

        if (unitHealth <= 0)
        {
            // Dying logic, ETC.
            Destroy(gameObject);
        }
    }

    internal void TakeDamage(int damageToInflict)
    {
        unitHealth -= damageToInflict;
        UpdateHealthUI();
    }
}
