using UnityEngine;

public class UnitController : MonoBehaviour
{
    Transform selectedUnit;
    bool unitSelected = false;
    GridManagerController gridManagerController;

    void Start()
    {
        gridManagerController = FindAnyObjectByType<GridManagerController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            bool hasHit = Physics.Raycast(ray, out hit);

            if (hasHit) {
                if (hit.transform.tag == "Tile") {
                    if (unitSelected) {
                        Vector2Int targetCords = hit.transform.GetComponent<Labeller>().cords;
                        Vector2Int startCords = new Vector2Int((int)selectedUnit.position.x, (int) selectedUnit.position.y / gridManagerController.UnityGridSize);

                        selectedUnit.transform.position = new Vector3(
                        targetCords.x * gridManagerController.UnityGridSize, 
                        selectedUnit.position.y, 
                        targetCords.y * gridManagerController.UnityGridSize);
                    }

                }

                if (hit.transform.tag == "Unit") {
                    selectedUnit = hit.transform;
                    unitSelected = true;
                    Debug.Log("Unit selected: " + selectedUnit.name);
                }
            }
        }   
    }
}
