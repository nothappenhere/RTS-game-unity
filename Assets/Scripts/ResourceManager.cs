using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance { get; set; }

    public int credits = 0;

    public List<BuildingType> allExistingBuildings;

    public PlacementSystem placementSystem;

    public enum ResourceType
    {
        Credits,
    }

    public event Action OnResourceChanged;
    public event Action OnBuildingsChanged;

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

    public void UpdateBuildingChanged(BuildingType buildingType, bool isNew, Vector3 position)
    {
        if (isNew)
        {
            allExistingBuildings.Add(buildingType);

            SoundManager.instance.PlayBuildingConstructionSound();
        }
        else
        {
            placementSystem.RemovePlacementData(position);
            allExistingBuildings.Remove(buildingType);
        }

        OnBuildingsChanged?.Invoke();
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

    public void SellingBuilding(BuildingType buildingType)
    {
        SoundManager.instance.PlayBuildingSellingSound();

        var sellingPrice = 0;

        foreach (ObjectData obj in DatabaseManager.instance.databaseSO.objectsData)
        {
            if (obj.thisBuildingType == buildingType)
            {
                foreach (BuildRequirement req in obj.resourceRequirements)
                {
                    if (req.resource == ResourceType.Credits)
                    {
                        sellingPrice = req.amount;
                    }
                }
            }
        }

        int amountToReturn = (int)(sellingPrice * 0.50);  // 50% of the cost

        IncreaseResource(ResourceType.Credits, amountToReturn);
    }


    private void UpdateUI()
    {
        creditsUI.text = $"{credits}";
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
        foreach (BuildRequirement req in objectData.resourceRequirements)
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
