using TMPro;
using UnityEngine;

public class Labeller : MonoBehaviour
{
    TextMeshPro label;
    public Vector2Int cords = new Vector2Int();
    GridManagerController gridManagerController;

    [SerializeField] Color defaultColor = Color.white; 
    [SerializeField] Color blockedColor = Color.red;
    [SerializeField] Color pathColor = new Color(1f, 0.5f, 0f);
    [SerializeField] Color exploredColor = Color.yellow;

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
        if (!Application.isPlaying)
        {
            label.enabled = true;
        }

        DisplayCords();
        transform.name = cords.ToString();

        SetLabelColor();
        ToggleLabel();
    }



    private void DisplayCords()
    {
        cords.x = Mathf.RoundToInt(transform.position.x / gridManagerController.UnityGridSize);
        cords.y = Mathf.RoundToInt(transform.position.z / gridManagerController.UnityGridSize);

        label.text = $"{cords.x} , {cords.y}";
    }

    void SetLabelColor() {
        if (gridManagerController == null) { return; }

        Node node = gridManagerController.GetNode(cords);

        if (node == null){ return; }

        else if (!node.isWalkable) {
            label.color = blockedColor;
        }
        else if (node.isPath) {
            label.color = pathColor;
        }
        else if (node.isExplored) {
            label.color = exploredColor;
        }
        else {
            label.color = defaultColor;
        }
    }

    void ToggleLabel() {
        if (Input.GetKeyDown(KeyCode.E)) {
            label.enabled = !label.IsActive();
        }
    }
}
