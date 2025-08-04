using UnityEngine;
using System.Collections.Generic;

public class TouchRecorder
{
    private readonly List<TouchLogEntry> log = new List<TouchLogEntry>();
    private float lastChangeTime;
    private bool isTouching = false;
    private Vector2 lastPos;

    public IReadOnlyList<TouchLogEntry> Log => log;

    public void Update()
    {
        bool currentlyTouching;
        Vector2 currentPos = Vector2.zero;

        // ✅ Real touch input
        if (Input.touchCount > 0)
        {
            currentlyTouching = true;
            currentPos = Input.GetTouch(0).position;
        }
        // ✅ Simulate touches using mouse input (Editor or Standalone builds)
        else if (Application.isEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            currentlyTouching = Input.GetMouseButton(0);
            if (currentlyTouching)
                currentPos = Input.mousePosition;
        }
        else
        {
            currentlyTouching = false;
        }

        // Detect state changes
        if (!isTouching && currentlyTouching)
        {
            Record(TouchAction.Down, currentPos);
        }
        else if (isTouching && currentlyTouching && (Vector2.Distance(currentPos, lastPos) > 0.5f))
        {
            Record(TouchAction.Move, currentPos);
        }
        else if (isTouching && !currentlyTouching)
        {
            Record(TouchAction.Up, lastPos);
        }

        isTouching = currentlyTouching;
        lastPos = currentPos;
    }

    private void Record(TouchAction action, Vector2 pos)
    {
        int deltaMs = Mathf.RoundToInt((Time.unscaledTime - lastChangeTime) * 1000f);
        log.Add(new TouchLogEntry(action, pos, deltaMs));
        lastChangeTime = Time.unscaledTime;
    }
}
