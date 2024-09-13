using UnityEngine;

public class ForestTiles : GridTileBase
{
    internal override void Init(Vector3Int coordinate)
    {
        gameObject.name = $"GrassTile ({coordinate.x}, {coordinate.y})";
    }
}
