using System;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour, IDamagaeble
{
    private float unitHealth; // Nyawa saat ini
    public float unitMaxHealth; // Nyawa maksimum unit

    public HealthTracker healthTracker; // UI slider kesehatan

    Animator animator;
    NavMeshAgent agent;

    void Start()
    {
        // Tambahkan unit ke daftar global saat muncul
        UnitSelectionManager.instance.allUnitsList.Add(gameObject);

        healthTracker.gameObject.SetActive(false);

        unitHealth = unitMaxHealth;
        UpdateHealthUI(); // Perbarui tampilan kesehatan awal

        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnDestroy()
    {
        // Hapus dari daftar unit saat dihancurkan
        UnitSelectionManager.instance.allUnitsList.Remove(gameObject);
    }

    private void UpdateHealthUI()
    {
        healthTracker.UpdateSliderValue(unitHealth, unitMaxHealth);

        if (unitHealth >= unitMaxHealth)
        {
            healthTracker.gameObject.SetActive(false);
        }

        if (unitHealth <= 0)
        {
            // Logika mati
            Destroy(gameObject);
        }
    }

    // Fungsi dipanggil saat unit terkena serangan
    public void TakeDamage(int damageToInflict)
    {
        if (!healthTracker.gameObject.activeSelf)
        {
            healthTracker.gameObject.SetActive(true); // Munculkan UI health ketika kena damage
        }

        unitHealth -= damageToInflict;
        UpdateHealthUI();
    }


    private void Update()
    {
        // Cek apakah unit sudah mencapai tujuan
        if (agent.remainingDistance > agent.stoppingDistance)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}
