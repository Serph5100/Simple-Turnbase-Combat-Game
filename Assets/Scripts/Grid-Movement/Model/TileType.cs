using Unity.VisualScripting;
using UnityEngine;



namespace Grid_Movement {
    [CreateAssetMenu(fileName = "Grid_Movement", menuName = "Grid_Movement/TileType")]
    [System.Serializable]
public class TileType : RuntimeScriptableObject
    {
        public string tileName;
        public GameObject tilePrefab;

        public bool isWalkable = true;

        protected override void OnReset()
        {

        }

    }

}
