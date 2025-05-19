using UnityEngine;
using UnityEngine.Tilemaps;

namespace Grid_Movement
{
    public class TileMap : MonoBehaviour
    {
        [Header("=== Tilemap ===")]
        public TileType[] tileTypes;
        int[,] tiles;

        [Header("=== Tile Size ===")]
        [SerializeField] int tileSize = 1;

        [Header("=== Map Size ===")]

        [SerializeField] int mapSizeX = 10;
        [SerializeField] int mapSizeY = 10;

        void Start()
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

            GenerateMapVisual();
        }

        void GenerateMapVisual()
        {
            for (int x = 0; x < mapSizeX; x++)
            {
                for (int y = 0; y < mapSizeY; y++)
                {
                    TileType tt = tileTypes[tiles[x, y]];
                    Instantiate(tt.tilePrefab, new Vector3(x * tileSize, 0, y * tileSize), Quaternion.identity);
                    //Debug.Log("Tile: " + tt.tileName + " at position: " + new Vector3(x * tileSize, y * tileSize, 0));

                }
            }
        
        }
}
}

