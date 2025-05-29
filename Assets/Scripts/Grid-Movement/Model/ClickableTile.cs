using Grid_Movement;
using UnityEngine;

public class ClickableTile : MonoBehaviour
{
    public int tileX;
    public int tileY;

    
    public TileMap map;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnMouseUp()
    {
        Debug.Log("Clicked.");

        map.GeneratePathTo(tileX, tileY);
    }


}
