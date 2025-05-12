using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;

    Transform selectedUnit;
    bool unitSelected = false;

    List<Node> path = new List<Node>();

    GridManagerController gridManagerController;
    Pathfinding pathFinding;

    void Start()
    {
        gridManagerController = FindAnyObjectByType<GridManagerController>();
        pathFinding = FindAnyObjectByType<Pathfinding>();
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
                        Vector2Int targetCords = hit.transform.GetComponent<Tile>().cords;
                        Vector2Int startCords = new Vector2Int((int)selectedUnit.transform.position.x / gridManagerController.UnityGridSize , (int) selectedUnit.transform.position.z / gridManagerController.UnityGridSize);

                        pathFinding.SetNewDestination(startCords, targetCords);
                        RecalculatePath(true);
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

    void RecalculatePath(bool resetPath) {
        Vector2Int coordinates = new Vector2Int();
        if (resetPath) {
            coordinates = pathFinding.StartCords;
        }
        else {
            coordinates = gridManagerController.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();
        path.Clear();
        path = pathFinding.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath() {
        for (int i = 1; i < path.Count; i++) {
            Vector3 targetPosition = gridManagerController.GetPositionFromCoordinates(path[i].cords);
            Vector3 startPosition = selectedUnit.position;
            float travelPercent = 0f;

            selectedUnit.LookAt(targetPosition);

            while (travelPercent < 1f) {
                travelPercent += Time.deltaTime * movementSpeed;
                selectedUnit.position = Vector3.Lerp(startPosition, targetPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
            
        }
    }
}
