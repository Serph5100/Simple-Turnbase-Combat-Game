using UnityEngine;

public class Node 
{
    public Vector2Int cords;
    public bool isWalkable;
    public bool isExplored;
    public bool isPath;
    public Node connectedToNode;

    public Node(Vector2Int cords, bool isWalkable = false)
    {
        this.cords = cords;
        this.isWalkable = isWalkable;
    }
}
