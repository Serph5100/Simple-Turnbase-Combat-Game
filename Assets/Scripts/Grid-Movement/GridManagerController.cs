using System.Collections.Generic;
using UnityEngine;

public class GridManagerController : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;
    [SerializeField] int unityGridSize;

    public int UnityGridSize { get { return unityGridSize; } }

    public Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    private void Awake()
    {
        CreateGrid();
    }

    public Node GetNode(Vector2Int cords) {
        if (grid.ContainsKey(cords))
        {
            return grid[cords];
        }
        
        return null;
    }

    public void BlockNode(Vector2Int cords) {
        if (grid.ContainsKey(cords))
        {
            grid[cords].isWalkable = false;
        }
    }

    public void ResetNode() {
        foreach (KeyValuePair<Vector2Int, Node> entry in grid) {
            entry.Value.connectedToNode = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position) {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.FloorToInt(position.x / unityGridSize);
        coordinates.y = Mathf.FloorToInt(position.z / unityGridSize);

        return coordinates;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int cords) {
        Vector3 position = new Vector3();

        position.x = cords.x * unityGridSize;
        position.z = cords.y * unityGridSize;

        return position;
    }

    void CreateGrid() {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int cords = new Vector2Int(x, y);
                grid.Add(cords, new Node(
                    cords, 
                    isWalkable: true
                ));

                //Debugging

                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(cords.x * unityGridSize, 0f, cords.y * unityGridSize);
                
            }
        }
    }
}
