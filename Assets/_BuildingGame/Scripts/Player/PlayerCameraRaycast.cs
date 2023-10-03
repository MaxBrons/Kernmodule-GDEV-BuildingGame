using UnityEngine;

public static class PlayerCameraRaycast
{
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
}
