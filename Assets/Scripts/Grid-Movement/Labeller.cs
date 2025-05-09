using TMPro;
using UnityEngine;

public class Labeller : MonoBehaviour
{
    TextMeshPro label;
    public Vector2Int cords = new Vector2Int();
    GridManagerController gridManagerController;

    private void Awake()
    {
        gridManagerController = FindAnyObjectByType<GridManagerController>();
        label = GetComponentInChildren<TextMeshPro>();

        if (gridManagerController == null)
        {
            Debug.LogError("GridManagerController not found in the scene.");
            return;
        }

        DisplayCords();

    }

    private void Update()
    {
        DisplayCords();
        transform.name = cords.ToString();
    }



    private void DisplayCords()
    {
        cords.x = Mathf.RoundToInt(transform.position.x / gridManagerController.UnityGridSize);
        cords.y = Mathf.RoundToInt(transform.position.z / gridManagerController.UnityGridSize);

        label.text = $"{cords.x} , {cords.y}";
    }
}
