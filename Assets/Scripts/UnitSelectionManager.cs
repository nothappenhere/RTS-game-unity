using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class UnitSelectionManager : MonoBehaviour
{
    public static UnitSelectionManager instance { get; set; } // Singleton instance agar dapat diakses dari script lain

    public List<GameObject> allUnitsList = new List<GameObject>(); // Daftar semua unit yang ada
    public List<GameObject> unitsSelected = new List<GameObject>(); // Daftar unit yang sedang dipilih

    public LayerMask clickable;   // Layer mask untuk objek yang bisa di-klik (unit)
    public LayerMask ground;      // Layer mask untuk tanah
    public LayerMask attackable;  // Layer mask untuk target yang bisa diserang
    public LayerMask constructable;  // Layer mask untuk bangunan

    public GameObject groundMaker; // Objek penanda lokasi klik kanan (tanah)
    private Camera cam;
    public bool attackCursorVisible; // Menyimpan apakah cursor attack ditampilkan

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


    private void Start()
    {
        // Mendapatkan referensi ke kamera utama
        cam = Camera.main;
    }

    private void Update()
    {
        // Proses klik kiri
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            // Jika klik pada objek dengan layer clickable (unit)
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    MultipleUnitSelection(hit.collider.gameObject); // Tambah/hapus unit dari seleksi
                }
                else
                {
                    SelectByClicking(hit.collider.gameObject); // Seleksi satu unit
                }
            }
            else
            {
                // Jika klik area kosong dan Shift tidak ditekan, batalkan semua seleksi
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    DeselectAll();
                }
            }
        }

        // Proses klik kanan untuk pergerakan unit ke tanah
        if (Input.GetMouseButtonDown(1) && unitsSelected.Count > 0)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                Vector3 fixedPosition = new Vector3(hit.point.x, 0.1f, hit.point.z); // Menetapkan posisi target di tanah
                groundMaker.SetActive(false);
                groundMaker.transform.position = fixedPosition;
                groundMaker.SetActive(true);
            }
        }

        // Menyerang target ketika mouse diarahkan ke musuh
        if (unitsSelected.Count > 0 && AtleastOneOffensiveUnit(unitsSelected))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, attackable))
            {
                Debug.Log("Enemy Hovered with mouse");
                attackCursorVisible = true;

                // Jika klik kanan saat mengarahkan ke musuh
                if (Input.GetMouseButtonDown(1))
                {
                    Transform target = hit.transform;

                    // Semua unit ofensif menyerang target
                    foreach (GameObject Unit in unitsSelected)
                    {
                        if (Unit.GetComponent<AttackController>())
                        {
                            Unit.GetComponent<AttackController>().target = target;
                        }
                    }
                }
            }
            else
            {
                attackCursorVisible = false;
            }
        }

        cursorSelector();
    }

    private void cursorSelector()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
        {
            CursorManager.Instance.SetMarkerType(CursorManager.CursorType.Selectable);
        } 
        else if (Physics.Raycast(ray, out hit, Mathf.Infinity, attackable)
            && unitsSelected.Count > 0 && AtleastOneOffensiveUnit(unitsSelected))
        {
            CursorManager.Instance.SetMarkerType(CursorManager.CursorType.Attackable);
        }
        else if (Physics.Raycast(ray, out hit, Mathf.Infinity, constructable) && unitsSelected.Count > 0)
        {
            CursorManager.Instance.SetMarkerType(CursorManager.CursorType.UnAvailable);
        }
        else if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground) && unitsSelected.Count > 0)
        {
            CursorManager.Instance.SetMarkerType(CursorManager.CursorType.Walkable);
        }
        else
        {
            CursorManager.Instance.SetMarkerType(CursorManager.CursorType.None);
        }
    }

    // Mengecek apakah ada setidaknya satu unit yang bisa menyerang
    private bool AtleastOneOffensiveUnit(List<GameObject> unitsSelected)
    {
        foreach (GameObject unit in unitsSelected)
        {
            if (unit != null &&  unit.GetComponent<AttackController>())
            {
                return true;
            }
        }
        return false;
    }

    // Menangani seleksi multi-unit (Shift + klik)
    private void MultipleUnitSelection(GameObject unit)
    {
        if (!unitsSelected.Contains(unit))
        {
            unitsSelected.Add(unit);
            TriggerSelectionIndicator(unit, true); // Tampilkan indikator
            EnableUnitMovement(unit, true);        // Aktifkan gerak
        }
        else
        {
            unitsSelected.Remove(unit);
            TriggerSelectionIndicator(unit, false);
            EnableUnitMovement(unit, false);
        }
    }

    // Membatalkan semua seleksi dan nonaktifkan indikator serta pergerakan unit
    public void DeselectAll()
    {
        foreach (var unit in allUnitsList)
        {
            EnableUnitMovement(unit, false);
            TriggerSelectionIndicator(unit, false);
        }
        groundMaker.SetActive(false);
        unitsSelected.Clear();
    }

    // Seleksi tunggal unit (klik tanpa Shift)
    private void SelectByClicking(GameObject unit)
    {
        DeselectAll();

        unitsSelected.Add(unit);

        TriggerSelectionIndicator(unit, true);
        EnableUnitMovement(unit, true);
    }

    // Aktifkan atau nonaktifkan script movement unit
    private void EnableUnitMovement(GameObject unit, bool trigger)
    {
        unit.GetComponent<UnitMovement>().enabled = trigger;
    }

    // Menyalakan atau mematikan visual indikator seleksi (anak pertama)
    private void TriggerSelectionIndicator(GameObject unit, bool isActive)
    {
        unit.transform.Find("Indicator").gameObject.SetActive(isActive);
    }

    // Seleksi unit melalui drag box
    internal void DragSelect(GameObject unit)
    {
        if (!unitsSelected.Contains(unit))
        {
            unitsSelected.Add(unit);
            TriggerSelectionIndicator(unit, true);
            EnableUnitMovement(unit, true);
        }
    }
}
