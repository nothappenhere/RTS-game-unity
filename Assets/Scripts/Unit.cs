using UnityEngine;

public class Unit : MonoBehaviour
{
    
    void Start()
    {
        UnitSelectionManager.instance.allUnitsList.Add(gameObject);
    }

    private void OnDestroy()
    {
        UnitSelectionManager.instance.allUnitsList.Remove(gameObject);
    }
}
