using System;
using UnityEngine;
using UnityEngine.UI;

public class BuySlot : MonoBehaviour
{
    public Sprite availableSprite;
    public Sprite unAvailableSprite;

    private bool isAvailable;

    public BuySystem buySystem;

    public int databaseItemID;

    private void Start()
    {
        // Subscribe to event/Listen to event
        ResourceManager.instance.OnResourceChanged += HandleResourceChanged;
        HandleResourceChanged();

        ResourceManager.instance.OnBuildingsChanged += HandleBuildingsChanged;
        HandleBuildingsChanged();
    }

    public void ClickedOnSlot()
    {
        if (isAvailable)
        {
            buySystem.placementSystem.StartPlacement(databaseItemID);
        }
    }

    private void UpdateAvailableUI()
    {
        if (isAvailable)
        {
            GetComponent<Image>().sprite = availableSprite;
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Image>().sprite = unAvailableSprite;
            GetComponent<Button>().interactable = false;
        }
    }

    //public void OnEnable()
    //{
        
    //}

    //public void OnDisable()
    //{
    //    // Unsubscribe to event/Stop listen to event
    //    ResourceManager.instance.OnResourceChanged -= HandleResourceChanged;

    //    // Unsubscribe to event/Stop listen to event
    //    // ResourceManager.instance.OnBuildingsChanged -= HandleBuildingsChanged;
    //}

    private void HandleResourceChanged()
    {
        ObjectData objectData = DatabaseManager.instance.databaseSO.objectsData[databaseItemID];

        bool requirementMet = true;

        foreach (BuildRequirement req in objectData.resourceRequirements)
        {
            if (ResourceManager.instance.GetResourceAmount(req.resource) < req.amount)
            {
                requirementMet = false;
                break;
            }
        }

        isAvailable = requirementMet;

        UpdateAvailableUI();
    }

    private void HandleBuildingsChanged()
    {
        ObjectData objectData = DatabaseManager.instance.databaseSO.objectsData[databaseItemID];

        foreach(BuildingType dependency in objectData.buildingDependecy)
        {
            // If the building has not dependencies
            if (dependency == BuildingType.None) 
            {
                gameObject.SetActive(true);
                return;
            }

            // Check if dependency exist
            if (!ResourceManager.instance.allExistingBuildings.Contains(dependency))
            {
                gameObject.SetActive(false);
                return;
            }
        }

        // If all requirements are met.
        gameObject.SetActive(true);
    }
}
