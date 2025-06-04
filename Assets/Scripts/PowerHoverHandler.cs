using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class PowerHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI powerText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (powerText != null)
        {
            powerText.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (powerText != null)
        {
            powerText.gameObject.SetActive(false);
        }
    }
}
