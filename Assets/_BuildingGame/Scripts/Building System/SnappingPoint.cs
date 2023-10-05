using System;
using UnityEngine;

[Flags]
public enum AllowedStructures : byte
{
    None = 0,
    Foundation = 1 << 1,
    Wall = 2 << 1,
};

[Serializable]
public struct SnappingPoint
{
    public Vector3 pos;
    public Vector3 rot;
    public AllowedStructures allowedStructures;
}
