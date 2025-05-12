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
        HandleResourceChanged();
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
    //    // Subscribe to event/Listen to event
    //    ResourceManager.instance.OnResourceChanged += HandleResourceChanged;
    //}

    //public void OnDisable()
    //{
    //    // Unsubscribe to event/Stop listen to event
    //    ResourceManager.instance.OnResourceChanged -= HandleResourceChanged;
    //}

    private void HandleResourceChanged()
    {
        ObjectData objectData = DatabaseManager.instance.databaseSO.objectsData[databaseItemID];

        bool requirementMet = true;

        foreach (BuildRequirement req in objectData.requirements)
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
}
