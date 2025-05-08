using UnityEngine;

public class AttackController : MonoBehaviour
{
    public Transform target;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && target == null)
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && target == null)
        {
            target = null;
        }
    }
}
