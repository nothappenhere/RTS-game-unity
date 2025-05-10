using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    Camera cam;
    NavMeshAgent agent; // Komponen Unity untuk navigasi otomatis
    public LayerMask ground; // Layer yang menentukan area tanah

    public bool isCommandedToMove; // Status apakah unit sedang bergerak

    private void Start()
    {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Klik kanan untuk memerintahkan unit bergerak
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                isCommandedToMove = true; // Unit menerima perintah gerak
                agent.SetDestination(hit.point); // Bergerak ke titik klik
            }
        }

        // Cek apakah unit sudah mencapai tujuan
        if (agent.hasPath == false || agent.remainingDistance <= agent.stoppingDistance)
        {
            isCommandedToMove = false; // Hentikan pergerakan
        }
    }
}
