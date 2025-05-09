using UnityEngine;

public class AttackController : MonoBehaviour
{
    public Transform target;

    public Material idleStateMaterial;
    public Material followStateMaterial;
    public Material attackStateMaterial;

    public bool isPlayer;
    public int unitDamage;

    private void OnTriggerEnter(Collider other)
    {
        if (isPlayer && other.CompareTag("Enemy") && target == null)
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPlayer &&  other.CompareTag("Enemy") && target == null)
        {
            target = null;
        }
    }

    public void setIdleMaterial()
    {
        GetComponent<Renderer>().material = idleStateMaterial; 
    }

    public void setFollowMaterial()
    {
        GetComponent<Renderer>().material = followStateMaterial;
    }

    public void setAttackMaterial()
    {
        GetComponent<Renderer>().material = attackStateMaterial;
    }

    private void OnDrawGizmos()
    {
        // Follow distance
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 10f * 0.3f);
        
        // Attack distance
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);

        // Stop Attack distance
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 1.2f);
    }
}
