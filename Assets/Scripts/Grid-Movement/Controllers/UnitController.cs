using System.Collections.Generic;
using Grid_Movement;
using UnityEngine;

namespace Grid_Movement
{
    public class UnitController : MonoBehaviour
    {
        public int unitX;
        public int unitY;
        public TileMap map;

        List<TileMap.Node> currentPath;

        void Update()
        {
            if (currentPath != null)
            {
                int currentNode = 0;
                while (currentNode < currentPath.Count - 1)
                {
                    Vector3 start = map.TileCordToWorldCord(currentPath[currentNode].x, currentPath[currentNode].y) + Vector3.up;
                    Vector3 end = map.TileCordToWorldCord(currentPath[currentNode + 1].x, currentPath[currentNode + 1].y) + Vector3.up;

                    Debug.DrawLine(start, end, Color.red);
                    currentNode++;
                }
            }
        }

        public void MoveToTile(int x, int y)
        {
            unitX = x;
            unitY = y;
            transform.position = new Vector3(x, 0, y);
        }

        public void ClearPath()
        {
            currentPath = null;
        }

        public void SetPath(List<TileMap.Node> path)
        {
            currentPath = path;
        }
    
    }

}
