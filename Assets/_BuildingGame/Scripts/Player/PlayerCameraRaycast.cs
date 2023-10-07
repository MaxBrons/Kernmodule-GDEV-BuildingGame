using UnityEngine;

public static class PlayerCameraRaycast
{
    /// <summary>
    /// Method return the point in worldspace where main camera is looking at
    /// </summary>
    public static Vector3 RaycastForPoint()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }

        return Vector3.one;
    }

    /// <summary>
    /// Method return the transform where main camera is looking at
    /// </summary>
    public static Transform RaycastForTransform()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            return hit.transform;
        }

        return null;
    }
}
