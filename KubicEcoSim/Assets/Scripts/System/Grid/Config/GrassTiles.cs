using UnityEngine;

public class GrassTiles : GridTileBase
{
    internal override void Init(Vector3Int coordinate) 
    {
        gameObject.name = $"GrassTile ({coordinate.x}, {coordinate.y})";
    }
}
