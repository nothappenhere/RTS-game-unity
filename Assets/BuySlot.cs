using System;
using UnityEngine;
using UnityEngine.UI;

public class BuySlot : MonoBehaviour
{
    public Sprite availableSprite;
    public Sprite unAvailableSprite;

    public bool isAvailable;

    public BuySystem buySystem;

    public int databaseItemID;

    private void Start()
    {
        UpdateAvailableUI();
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
}
