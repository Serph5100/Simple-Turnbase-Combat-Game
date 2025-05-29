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

        [SerializeField] float unitSpeed = 1f;

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

        public void MoveToNextTile()
        {
            float remainingMovement = unitSpeed;

            while (remainingMovement > 0)
            {
                if (currentPath == null || currentPath.Count == 0) return;

            
            remainingMovement -= map.CostToEnterTile(currentPath[0].x, currentPath[0].y, currentPath[1].x, currentPath[1].y);

            //Set unit position to next node
            unitX = currentPath[1].x;
            unitY = currentPath[1].y;

            //Move unit to next node in currentPath[0]
            //Move Instantaneously
            transform.position = map.TileCordToWorldCord(unitX, unitY);
            
            //Remove current node
            currentPath.RemoveAt(0);

            //Destination reached
            if (currentPath.Count == 1)
            {
                    ClearPath();

                }
                
            }
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
