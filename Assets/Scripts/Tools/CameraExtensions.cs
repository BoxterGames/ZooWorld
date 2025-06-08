using UnityEngine;

public static class CameraExtensions
{
    public static bool IsOutsideView(this Camera camera, Transform transform)
    {
        var viewportPoint = camera.WorldToViewportPoint(transform.position);

        return viewportPoint.x < 0f || viewportPoint.x > 1f ||
               viewportPoint.y < 0f || viewportPoint.y > 1f;
    }
}
