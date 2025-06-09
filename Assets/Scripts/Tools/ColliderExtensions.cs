using System.Collections.Generic;
using UnityEngine;

public static class ColliderExtensions
{
    public static bool IsCollide(this BoxCollider self, IEnumerable<BoxCollider> other, float minDistance)
    {
        foreach (var i in other)
        {
            if (self == i ||
                !self.IsIntersect(i, out var point) ||
                (point - self.transform.position).magnitude > minDistance)
            {
                continue;
            }

            return true;
        }

        return false;
    }

    public static bool IsIntersect(this BoxCollider self, BoxCollider other, out Vector3 intersectionPoint)
    {
        var fwdDirA = self.transform.forward;
        var a = self.transform.position;

        var halfSize = other.transform.lossyScale * 0.5f;
        var fwdDirB = other.transform.forward * halfSize.z;
        var rightDirB = other.transform.right * halfSize.x;

        var centerB = other.transform.position;
        var fwdB = centerB + fwdDirB;
        var backB = centerB - fwdDirB;
        var rightB = centerB + rightDirB;
        var leftB = centerB - rightDirB;

        var points = new List<Vector3>();
        var point = Vector3.zero;
        
        if (IsIntersect(fwdB, a, rightDirB, fwdDirA, out point))
        {
            points.Add(point);
        }
        
        if (IsIntersect(backB, a, rightDirB, fwdDirA, out point))
        {
            points.Add(point);
        }

        if (IsIntersect(rightB, a, fwdDirB, fwdDirA, out point))
        {
            points.Add(point);
        }
        
        if (IsIntersect(leftB, a, fwdDirB, fwdDirA, out point))
        {
            points.Add(point);
        }

        if (points.Count == 0)
        {
            intersectionPoint = Vector3.zero;
            return false;
        }

        intersectionPoint = points[0];
        for (int i = 1; i < points.Count; i++)
        {
            if ((a - points[i]).sqrMagnitude < (a - intersectionPoint).sqrMagnitude)
            {
                intersectionPoint = points[i];
            }
        }

        return true;
    }

    private static bool IsIntersect(Vector3 a, Vector3 b, Vector3 aDir, Vector3 bDir, out Vector3 result)
    {
        var distance = aDir.x * bDir.z - aDir.z * bDir.x;

        if (Mathf.Abs(distance) < 0.0001f)
        {
            result = Vector3.zero;
            return false;
        }

        var delta = b - a;
        var t = (delta.x * bDir.z - delta.z * bDir.x) / distance;
        result = a + t * aDir;
        return t >= -1 && t <= 1;
    }
}
