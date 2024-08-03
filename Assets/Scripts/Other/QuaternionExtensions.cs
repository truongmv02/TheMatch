using UnityEngine;
public static class QuaternionExtensions
{

    public static Quaternion Vector2ToRotation(Vector2 direction)
    {
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.Euler(0f, 0f, angle);
        return rotation;
    }
}