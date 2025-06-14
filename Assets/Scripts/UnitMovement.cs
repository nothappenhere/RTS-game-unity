using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    Camera cam;
    NavMeshAgent agent; // Komponen Unity untuk navigasi otomatis
    public LayerMask ground; // Layer yang menentukan area tanah

    public bool isCommandedToMove; // Status apakah unit sedang bergerak

    DirectionIndicator directionIndicator;

    private void Start()
    {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();

        directionIndicator = GetComponent<DirectionIndicator>();
    }

    private void Update()
    {
        // Klik kanan untuk memerintahkan unit bergerak
        if (Input.GetMouseButtonDown(1) && IsMovingPosible())
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                isCommandedToMove = true; // Unit menerima perintah gerak
                StartCoroutine(NoCommand());
                agent.SetDestination(hit.point); // Bergerak ke titik klik

                directionIndicator.DrawLine(hit);
            }
        }

        // Cek apakah unit sudah mencapai tujuan
        //if (agent.hasPath == false || agent.remainingDistance <= agent.stoppingDistance)
        //{
        //    isCommandedToMove = false; // Hentikan pergerakan
        //}
    }

    private bool IsMovingPosible()
    {
        return CursorManager.Instance.currentCursor != CursorManager.CursorType.UnAvailable;
    }

    IEnumerator NoCommand()
    {
        yield return new WaitForSeconds(1);
        isCommandedToMove = false; // Hentikan pergerakan
    }
}
