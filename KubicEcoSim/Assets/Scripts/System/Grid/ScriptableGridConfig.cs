using UnityEngine;

[CreateAssetMenu]
public class ScriptableGridConfig : ScriptableObject
{
    public GridType Type;
    [Space(10)]
    public GridLayout.CellLayout Layout;
    public GridTileBase GrassPrefab, ForestPrefab;
    public Vector3 CellSize;
    public GridLayout.CellSwizzle GridSwizzle;
}

