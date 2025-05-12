using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] Vector2Int startCords;
    public Vector2Int StartCords { get { return startCords; } }
    [SerializeField] Vector2Int targetCords;
    public Vector2Int TargetCords { get { return targetCords; } }

    Node startNode;
    Node targetNode;
    Node currentNode;

    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

    GridManagerController gridManagerController;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    Vector2Int[] searchOrder = {
        Vector2Int.right,
        Vector2Int.left,
        Vector2Int.up,
        Vector2Int.down
    };

    private void Awake()
    {
        gridManagerController = FindAnyObjectByType<GridManagerController>();
        if (gridManagerController != null)
        {
            grid = gridManagerController.grid;
        }


    }

    public List<Node> GetNewPath() {
        return GetNewPath(startCords);
    } 

    public List<Node> GetNewPath(Vector2Int cords) {
        gridManagerController.ResetNode();

        BreadthFirstSearch(cords);
        return BuildPath();
    }

    void BreadthFirstSearch(Vector2Int cords) {
        startNode.isWalkable = true;
        targetNode.isWalkable = true;

        frontier.Clear();
        reached.Clear();

        bool isTraveling = true;

        frontier.Enqueue(grid[cords]);
        reached.Add(cords, grid[cords]);

        while (frontier.Count > 0 && isTraveling) {
            currentNode = frontier.Dequeue();
            currentNode.isExplored = true;
            ExploreNeighbors();
            if (currentNode.cords == targetCords) {
                isTraveling = false;
                currentNode.isWalkable = false;
            }
        }
    }

    void ExploreNeighbors() {
        List<Node> neighbors = new List<Node>();

        foreach (Vector2Int direction in searchOrder) {
            Vector2Int neighborCords = currentNode.cords + direction;
            if (grid.ContainsKey(neighborCords)) {
                Node neighborNode = grid[neighborCords];
                neighbors.Add(grid[neighborCords]);
            }
        }

        foreach (Node neighbor in neighbors) {
            if (!reached.ContainsKey(neighbor.cords) && neighbor.isWalkable) {
                neighbor.connectedToNode = currentNode;
                reached.Add(neighbor.cords, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }
    
    List<Node> BuildPath() {
        List<Node> path = new List<Node>();
        Node currentNode = targetNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while (currentNode.connectedToNode != null) {
            currentNode = currentNode.connectedToNode;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse();
        return path;
    }

    public void NotifyReceivers() {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }

    public void SetNewDestination(Vector2Int startCoordinates, Vector2Int targetCoordinates) {
        startCords = startCoordinates;
        targetCords = targetCoordinates;

        startNode = grid[this.startCords];
        targetNode = grid[this.targetCords];
        GetNewPath();
    }

    
}
