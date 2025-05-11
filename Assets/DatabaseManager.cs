using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager instance { get; set; }

    // OBJECTS DATABASE
    public ObjectsDatabseSO databaseSO;


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

    

}
