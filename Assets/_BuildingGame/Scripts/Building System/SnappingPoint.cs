using UnityEngine;

public struct SnappingPoint
{
    public Vector3 position;

    public enum AllowedSnap : short
    {
        Foundation = 0,
        Wall = 1,
    }
}
