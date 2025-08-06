using System.Collections.Generic;
using UnityEngine;

public class TouchRecorder : ITouchRecorder
{
    private readonly List<TouchLogEntry> log = new List<TouchLogEntry>();
    private float elapsedTime = 0f;
    private float lastChangeTime = 0f;
    private bool isTouching = false;
    private Vector2 lastPos;

    public IReadOnlyList<TouchLogEntry> Log => log;

    public void Update(float deltaTime)
    {
        elapsedTime += deltaTime;

#if UNITY_EDITOR || UNITY_ANDROID || UNITY_IOS
        bool currentlyTouching = false;
        Vector2 currentPos = Vector2.zero;

        if (Input.touchCount > 0)
        {
            currentlyTouching = true;
            currentPos = Input.GetTouch(0).position;
        }
        else if (Application.isEditor && Input.GetMouseButton(0))
        {
            currentlyTouching = true;
            currentPos = Input.mousePosition;
        }

        if (!isTouching && currentlyTouching)
        {
            Record(TouchAction.Down, currentPos);
        }
        else if (isTouching && currentlyTouching && Vector2.Distance(currentPos, lastPos) > 0.5f)
        {
            Record(TouchAction.Move, currentPos);
        }
        else if (isTouching && !currentlyTouching)
        {
            Record(TouchAction.Up, lastPos);
        }

        isTouching = currentlyTouching;
        lastPos = currentPos;
#endif
    }

    public void SimulateTouch(TouchAction action, Vector2 position, int deltaMs = 0)
    {
        log.Add(new TouchLogEntry(action, position, deltaMs));
    }

    private void Record(TouchAction action, Vector2 pos)
    {
        int deltaMs = Mathf.RoundToInt((elapsedTime - lastChangeTime) * 1000f);
        log.Add(new TouchLogEntry(action, pos, deltaMs));
        lastChangeTime = elapsedTime;
    }
}
