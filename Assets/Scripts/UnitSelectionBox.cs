using UnityEngine;

public class UnitSelectionBox : MonoBehaviour
{
    Camera myCam;

    [SerializeField]
    RectTransform boxVisual; // UI box visual untuk seleksi unit secara drag

    Rect selectionBox; // Rect representasi area seleksi
    Vector2 startPosition;
    Vector2 endPosition;

    private void Start()
    {
        boxVisual.gameObject.SetActive(true);

        myCam = Camera.main;
        startPosition = Vector2.zero;
        endPosition = Vector2.zero;
        DrawVisual(); // Inisialisasi tampilan awal box
    }

    private void Update()
    {
        // Ketika klik mouse kiri
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
            selectionBox = new Rect(); // Inisialisasi area seleksi
        }

        // Ketika menahan klik kiri (drag)
        if (Input.GetMouseButton(0))
        {
            if (boxVisual.rect.width > 0 || boxVisual.rect.height > 0)
            {
                UnitSelectionManager.instance.DeselectAll(); // Reset seleksi saat drag dimulai
                SelectUnits(); // Seleksi unit dalam area
            }

            endPosition = Input.mousePosition;
            DrawVisual();    // Gambar ulang box seleksi visual
            DrawSelection(); // Hitung ulang area seleksi
        }

        // Ketika melepas mouse kiri
        if (Input.GetMouseButtonUp(0))
        {
            SelectUnits(); // Finalisasi seleksi unit
            startPosition = Vector2.zero;
            endPosition = Vector2.zero;
            DrawVisual(); // Reset box visual
        }
    }

    void DrawVisual()
    {
        // Calculate the starting and ending positions of the selection box.
        Vector2 boxStart = startPosition;
        Vector2 boxEnd = endPosition;

        // Calculate the center of the selection box.
        Vector2 boxCenter = (boxStart + boxEnd) / 2;

        // Set the position of the visual selection box based on its center.
        boxVisual.position = boxCenter;

        // Calculate the size of the selection box in both width and height.
        Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));

        // Set the size of the visual selection box based on its calculated size.
        boxVisual.sizeDelta = boxSize;
    }

    void DrawSelection()
    {
        // Hitung batas X dari kotak seleksi
        if (Input.mousePosition.x < startPosition.x)
        {
            selectionBox.xMin = Input.mousePosition.x;
            selectionBox.xMax = startPosition.x;
        }
        else
        {
            selectionBox.xMin = startPosition.x;
            selectionBox.xMax = Input.mousePosition.x;
        }

        // Hitung batas Y dari kotak seleksi
        if (Input.mousePosition.y < startPosition.y)
        {
            selectionBox.yMin = Input.mousePosition.y;
            selectionBox.yMax = startPosition.y;
        }
        else
        {
            selectionBox.yMin = startPosition.y;
            selectionBox.yMax = Input.mousePosition.y;
        }
    }

    // Seleksi unit yang berada di dalam area seleksi
    void SelectUnits()
    {
        foreach (var unit in UnitSelectionManager.instance.allUnitsList)
        {
            if (selectionBox.Contains(myCam.WorldToScreenPoint(unit.transform.position)))
            {
                UnitSelectionManager.instance.DragSelect(unit);
            }
        }
    }
}
