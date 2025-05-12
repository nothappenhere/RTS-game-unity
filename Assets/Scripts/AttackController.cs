using UnityEngine;

public class AttackController : MonoBehaviour
{
    public Transform target; // Target musuh saat ini

    public Material idleStateMaterial;
    public Material followStateMaterial;
    public Material attackStateMaterial;

    public bool isPlayer; // Apakah ini unit pemain?
    public int unitDamage; // Besarnya damage saat menyerang

    public GameObject spearEffect;

    private void OnTriggerEnter(Collider other)
    {
        // Jika bertemu musuh dan belum punya target, tetapkan target
        if (isPlayer && other.CompareTag("Enemy") && target == null)
        {
            target = other.transform;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        // Jika bertemu musuh dan belum punya target, tetapkan target
        if (isPlayer && other.CompareTag("Enemy") && target == null)
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Jika musuh keluar dari trigger dan target belum ditetapkan, kosongkan target
        if (isPlayer &&  other.CompareTag("Enemy") && target == null)
        {
            target = null;
        }
    }

    // Ganti material saat idle
    public void setIdleMaterial()
    {
        //GetComponent<Renderer>().material = idleStateMaterial;
    }

    // Ganti material saat mengikuti target
    public void setFollowMaterial()
    {
        //GetComponent<Renderer>().material = followStateMaterial;
    }

    // Ganti material saat menyerang
    public void setAttackMaterial()
    {
        //GetComponent<Renderer>().material = attackStateMaterial;
    }

    private void OnDrawGizmos()
    {
        // Visualisasi jarak interaksi
        Gizmos.color = Color.yellow; // Jarak follow
        Gizmos.DrawWireSphere(transform.position, 3f * 0.3f);

        Gizmos.color = Color.red; // Jarak attack
        Gizmos.DrawWireSphere(transform.position, 2f);

        Gizmos.color = Color.blue; // Jarak stop attack
        Gizmos.DrawWireSphere(transform.position, 2.5f);
    }
}
