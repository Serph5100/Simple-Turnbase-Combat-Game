using Unity.VisualScripting;
using UnityEngine;



namespace Grid_Movement {
    [CreateAssetMenu(fileName = "Grid_Movement", menuName = "Grid_Movement/TileType")]
    [System.Serializable]
public class TileType : RuntimeScriptableObject
    {
        public string tileName;
        public GameObject tilePrefab;
        [SerializeField] float movementCost = 1f;

        public bool isWalkable = true;

        public float GetMovementCost()
        {
            return movementCost;
        }

        protected override void OnReset()
        {

        }

    }

}
