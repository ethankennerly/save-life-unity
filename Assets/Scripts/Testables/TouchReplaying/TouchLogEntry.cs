using UnityEngine;

[System.Serializable]
public struct TouchLogEntry
{
    public TouchAction Action;
    public Vector2 NormalizedPos; // fraction of screen size
    public int DeltaMs; // time since last change in ms

    public TouchLogEntry(TouchAction action, Vector2 screenPos, int deltaMs)
    {
        Action = action;
        NormalizedPos = new Vector2(
            screenPos.x / Screen.width,
            screenPos.y / Screen.height
        );
        DeltaMs = deltaMs;
    }

    public Vector2 GetScreenPos()
    {
        return new Vector2(
            NormalizedPos.x * Screen.width,
            NormalizedPos.y * Screen.height
        );
    }
}
