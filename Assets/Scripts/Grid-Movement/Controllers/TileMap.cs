using System.Collections.Generic;
using System.Linq;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Grid_Movement
{

    public class TileMap : MonoBehaviour
    {
        public class Node
        {
            public List<Node> neighbors;
            public int x;
            public int y;

            public Node()
            {
                neighbors = new List<Node>();
            }

            public float DistanceTo(Node other)
            {
                return Vector2.Distance(
                    new Vector2(x, y),
                    new Vector2(other.x, other.y)
                );
            }
        }
        // ========================== NODE CLASS ==========================
        Node[,] graph;

        // ========================== PATHFINDING ==========================



        [Header("=== Unit ===")]
        public GameObject selectedUnit;

        [Header("=== Tilemap ===")]
        public TileType[] tileTypes;
        int[,] tiles;

        [Header("=== Tile Size ===")]
        [SerializeField] int tileSize = 1;

        [Header("=== Map Size ===")]

        [SerializeField] int mapSizeX = 10;
        [SerializeField] int mapSizeY = 10;

        [Header("=== Units ===")]
        UnitController[,] units;



        void Start()
        {
            //Player Unit starting position
            selectedUnit.GetComponent<UnitController>().unitX = (int)selectedUnit.transform.position.x;
            selectedUnit.GetComponent<UnitController>().unitY = (int)selectedUnit.transform.position.z;
            selectedUnit.GetComponent<UnitController>().map = this;

            GenerateMapData();
            GenerateMapVisual();
            GeneratePathfindingGraph();
        }

        void GenerateMapData()
        {
            // Initialize the tileTypes array
            tiles = new int[mapSizeX, mapSizeY];

            // Initialize Map's tiles
            for (int x = 0; x < mapSizeX; x++)
            {
                for (int y = 0; y < mapSizeY; y++)
                {
                    tiles[x, y] = 0;

                }
            }

            //Debug :
            tiles[4, 4] = 2;
            tiles[5, 4] = 2;
            tiles[6, 4] = 2;
            tiles[7, 4] = 2;
            tiles[8, 4] = 2;

            tiles[4, 5] = 2;
            tiles[4, 6] = 2;
            tiles[8, 5] = 2;
            tiles[8, 6] = 2;

        }


        public float CostToEnterTile(int sourceX, int sourceY, int targetX, int targetY)
        {
            TileType tt = tileTypes[tiles[targetX, targetY]];

            float cost = tt.GetMovementCost();

            if (sourceX != targetX && sourceY != targetY)
            {
                cost += 0.001f; // Diagonal movement cost
            }
            
            return cost;
        }

        void GenerateMapVisual()
        {
            for (int x = 0; x < mapSizeX; x++)
            {
                for (int y = 0; y < mapSizeY; y++)
                {
                    TileType tt = tileTypes[tiles[x, y]];
                    GameObject go = Instantiate(tt.tilePrefab, new Vector3(x * tileSize, 0, y * tileSize), Quaternion.identity);
                    //Debug.Log("Tile: " + tt.tileName + " at position: " + new Vector3(x * tileSize, y * tileSize, 0));
                    ClickableTile ct = go.GetComponent<ClickableTile>();
                    ct.tileX = x;
                    ct.tileY = y;
                    ct.map = this;
                    ct.gameObject.name = tt.tileName + " (" + x + ", " + y + ")";

                }
            }

        }

        void GeneratePathfindingGraph()
        {
            graph = new Node[mapSizeX, mapSizeY];

            // Initialize the graph
            for (int x = 0; x < mapSizeX; x++)
            {
                for (int y = 0; y < mapSizeY; y++)
                {
                    graph[x, y] = new Node();
                    graph[x, y].x = x;
                    graph[x, y].y = y;
                }
            }
            

            //Initialize Connected Node (4 directions)
            for (int x = 0; x < mapSizeX; x++)
            {
                for (int y = 0; y < mapSizeY; y++)
                {
                    if (x > 0)
                    {
                        graph[x, y].neighbors.Add(graph[x - 1, y]);
                    }
                    if (x < mapSizeX - 1)
                    {
                        graph[x, y].neighbors.Add(graph[x + 1, y]);
                    }
                    if (y > 0)
                    {
                        graph[x, y].neighbors.Add(graph[x, y - 1]);
                    }
                    if (y < mapSizeY - 1)
                    {
                        graph[x, y].neighbors.Add(graph[x, y + 1]);
                    }

                }
            }
                
            
        }

        public Vector3 TileCordToWorldCord(int x, int y)
        {
            return new Vector3(x * tileSize, 0, y * tileSize);
        }


        public bool UnitCanMoveTo(int x, int y)
        {
            TileType tt = tileTypes[tiles[x, y]];
            if (tt.isWalkable)
            {
                return true;
            }
            return false;
        }

        public void GeneratePathTo(int x, int y)
        {

            /* selectedUnit.GetComponent<UnitController>().unitX = x;
            selectedUnit.GetComponent<UnitController>().unitY = y;
            selectedUnit.transform.position = TileCordToWorldCord(x, y); */

            if (!UnitCanMoveTo(x, y))
            {
                Debug.Log("Cannot move to this tile");
                return;
            }

            selectedUnit.GetComponent<UnitController>().ClearPath();

            // Dijkstra's Algorithm for pathfinding


            Dictionary<Node, float> gScore = new Dictionary<Node, float>();
            Dictionary<Node, float> fScore = new Dictionary<Node, float>();
            Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();
            PriorityQueue<Node, float> openSet = new PriorityQueue<Node, float>();

            Node startNode = graph[selectedUnit.GetComponent<UnitController>().unitX, selectedUnit.GetComponent<UnitController>().unitY];
            Node endNode = graph[x, y];

            // Initialize scores
            foreach (Node node in graph)
            {
            gScore[node] = float.MaxValue;
            fScore[node] = float.MaxValue;
            cameFrom[node] = null;
        }
            gScore[startNode] = 0;
            fScore[startNode] = startNode.DistanceTo(endNode);
            openSet.Enqueue(startNode, fScore[startNode]);

            while (openSet.Count > 0)
            {
            Node current = openSet.Dequeue();

            // Early exit if goal is reached
            if (current == endNode)
            {
            break;
            }

        foreach (Node neighbor in current.neighbors)
        {
            // Calculate tentative gScore (current cost + movement cost)
            float tentative_gScore = gScore[current] + CostToEnterTile(current.x, current.y, neighbor.x, neighbor.y);

            // Update if this path is better
            if (tentative_gScore < gScore[neighbor])
            {
                cameFrom[neighbor] = current;
                gScore[neighbor] = tentative_gScore;
                fScore[neighbor] = tentative_gScore + neighbor.DistanceTo(endNode);

                // Add to openSet
                openSet.Enqueue(neighbor, fScore[neighbor]);
            }
        }
    }

    // Reconstruct path (same as before)
    if (gScore[endNode] == float.MaxValue)
    {
        Debug.Log("No path found");
        return;
    }

    List<Node> currentPath = new List<Node>();
    Node currentNode = endNode;
    while (cameFrom[currentNode] != null)
    {
        currentPath.Add(currentNode);
        currentNode = cameFrom[currentNode];
    }
    currentPath.Reverse();

    selectedUnit.GetComponent<UnitController>().SetPath(currentPath);
        }





    }       
}

