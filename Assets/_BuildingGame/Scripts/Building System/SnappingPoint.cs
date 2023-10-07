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
    public Vector3 posistion;
    public Vector3 rotation;
    public AllowedStructures allowedStructures;
}
