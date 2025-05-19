using UnityEngine;
using UnityEngine.Tilemaps;

namespace Grid_Movement
{
    public class TileMap : MonoBehaviour
{
    public TileType[] tileTypes;
    Tile[,] tiles;

    [SerializeField] int mapSizeX = 10;
    [SerializeField] int mapSizeY = 10;

    void Start()
    {
        // Initialize the tileTypes array
        tiles = new Tile[mapSizeX, mapSizeY];

        // Initialize Map's tiles
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                tiles[x, y] = new Tile();

            }
        }
    }
}
}

