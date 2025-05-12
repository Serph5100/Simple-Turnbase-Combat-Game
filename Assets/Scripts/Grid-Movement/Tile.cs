using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] bool obstacle = false;
    public Vector2Int cords;

    GridManagerController gridManagerController;

    void Start()
    {
        SetCords();
        if (obstacle) {
            gridManagerController.BlockNode(cords);
        }
    }

    void SetCords() {
        gridManagerController = FindAnyObjectByType<GridManagerController>();
        int x = (int)transform.position.x;
        int z = (int)transform.position.z;

        cords = new Vector2Int(x / gridManagerController.UnityGridSize, z / gridManagerController.UnityGridSize);
    }
}
