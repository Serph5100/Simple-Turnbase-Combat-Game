using UnityEngine;

public class UnitController : MonoBehaviour
{
    public int unitX;
    public int unitY;

    public void MoveToTile(int x, int y)
    {
        unitX = x;
        unitY = y;
        transform.position = new Vector3(x, 0 , y);
    }
}
