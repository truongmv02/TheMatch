using UnityEngine;

public static class AngleUtilities
{
    public static float AngleFormFacingDirection(Transform receiver, Transform source, int direction)
    {
        return Vector2.SignedAngle(
            Vector2.right * direction, source.position - receiver.position) * direction;
    }
}