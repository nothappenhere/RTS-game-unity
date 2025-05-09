using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private float unitHealth; // Nyawa saat ini
    public float unitMaxHealth; // Nyawa maksimum unit

    public HealthTracker healthTracker; // UI slider kesehatan


    void Start()
    {
        // Tambahkan unit ke daftar global saat muncul
        UnitSelectionManager.instance.allUnitsList.Add(gameObject);

        unitHealth = unitMaxHealth;
        UpdateHealthUI(); // Perbarui tampilan kesehatan awal
    }

    private void OnDestroy()
    {
        // Hapus dari daftar unit saat dihancurkan
        UnitSelectionManager.instance.allUnitsList.Remove(gameObject);
    }

    private void UpdateHealthUI()
    {
        healthTracker.UpdateSliderValue(unitHealth, unitMaxHealth);

        if (unitHealth <= 0)
        {
            // Logika mati
            Destroy(gameObject);
        }
    }

    // Fungsi dipanggil saat unit terkena serangan
    internal void TakeDamage(int damageToInflict)
    {
        unitHealth -= damageToInflict;
        UpdateHealthUI();
    }
}
