using UnityEngine;

public static class CameraExtensions
{
    public static bool IsOutsideView(this Camera camera, Transform transform, float offset = 0.1f)
    {
        var viewportPoint = camera.WorldToViewportPoint(transform.position);

        return viewportPoint.x < offset || viewportPoint.x > 1 - offset ||
               viewportPoint.y < offset || viewportPoint.y > 1 - offset;
    }
    
    public static Vector3 GetPointInCameraView(this Camera camera, float resultY, float offset = 0.3f)
    {
        var topLeft = GetPosition(camera, new Vector3(offset, 1 - offset), resultY);
        var rightDown = GetPosition(camera, new Vector3(1 - offset, offset), resultY);

        return new Vector3(Random.Range(topLeft.x, rightDown.x),
                           Random.Range(topLeft.y, rightDown.y),
                           Random.Range(topLeft.z, rightDown.z));
    }

    private static Vector3 GetPosition(Camera camera, Vector3 direction, float resultY)
    {
        var ray = camera.ViewportPointToRay(direction);
        var t = (resultY - ray.origin.y) / ray.direction.y;
        return ray.origin + ray.direction * t;
    }
}
