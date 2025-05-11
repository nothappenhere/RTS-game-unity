using System;
using TMPro;
using UnityEditor;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance { get; set; }

    public int credits = 100;
    public enum ResourceType
    {
        Credits,
    }

    public event Action OnResourceChanged;

    public TextMeshProUGUI creditsUI;

    private void Awake()
    {
        // Menetapkan singleton instance dan menghancurkan duplikat jika ada
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        UpdateUI();
    }

    public void IncreaseResource(ResourceType resource, int amountToIncrease)
    {
        switch (resource)
        {
            case ResourceType.Credits:
                credits += amountToIncrease;
                break;
            default:
                break;
        }

        OnResourceChanged?.Invoke();
    }

    public void DecreaseResource(ResourceType resource, int amountToDecrease)
    {
        switch (resource)
        {
            case ResourceType.Credits:
                credits -= amountToDecrease;
                break;
            default:
                break;
        }

        OnResourceChanged?.Invoke();
    }

    private void UpdateUI()
    {
        creditsUI.text = $"Credits: {credits}";
    }

    public int GetCredits()
    {
        return credits;
    }

    internal int GetResourceAmount(ResourceType resource)
    {
        switch (resource)
        {
            case ResourceType.Credits:
                return credits;
            default:
                break;
        }

        return 0;
    }

    internal void DecreaseResourceBasedOnRequirement(ObjectData objectData)
    {
        foreach (BuildRequirement req in objectData.requirements)
        {
            DecreaseResource(req.resource, req.amount);
        }
    }

    private void OnEnable()
    {
        OnResourceChanged += UpdateUI;
    }

    private void OnDisable()
    {
        OnResourceChanged -= UpdateUI;
    }
}
