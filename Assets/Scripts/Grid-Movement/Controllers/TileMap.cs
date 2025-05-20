using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Grid_Movement
{
    public class TileMap : MonoBehaviour
    {
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
            GenerateMapData();
            GenerateMapVisual();
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
        
        public Vector3 TileCordToWorldCord(int x, int y)
        {
            return new Vector3(x * tileSize, 0, y * tileSize);
        }

        public void MoveSelectedUnitTo(int x, int y)
        {
            selectedUnit.GetComponent<UnitController>().unitX = x;
            selectedUnit.GetComponent<UnitController>().unitY = y;
            selectedUnit.transform.position = TileCordToWorldCord(x, y);

        }
}
}

