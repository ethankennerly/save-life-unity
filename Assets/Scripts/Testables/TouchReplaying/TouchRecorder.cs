using System.Collections.Generic;
using UnityEngine;

public class TouchRecorder : ITouchRecorder
{
    private readonly List<TouchLogEntry> _log = new List<TouchLogEntry>();
    private float _elapsedTime = 0f;
    private float _lastChangeTime = 0f;
    private bool _isTouching = false;
    private Vector2 _lastPos;

    public IReadOnlyList<TouchLogEntry> Log => _log;

    public void Update(float deltaTime)
    {
        _elapsedTime += deltaTime;

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

        if (!_isTouching && currentlyTouching)
        {
            Record(TouchAction.Down, currentPos);
        }
        else if (_isTouching && currentlyTouching && Vector2.Distance(currentPos, _lastPos) > 0.5f)
        {
            Record(TouchAction.Move, currentPos);
        }
        else if (_isTouching && !currentlyTouching)
        {
            Record(TouchAction.Up, _lastPos);
        }

        _isTouching = currentlyTouching;
        _lastPos = currentPos;
#endif
    }

    public void SimulateTouch(TouchAction action, Vector2 position, int deltaMs = 0)
    {
        _log.Add(new TouchLogEntry(action, position, deltaMs));
    }

    private void Record(TouchAction action, Vector2 pos)
    {
        int deltaMs = Mathf.RoundToInt((_elapsedTime - _lastChangeTime) * 1000f);
        _log.Add(new TouchLogEntry(action, pos, deltaMs));
        _lastChangeTime = _elapsedTime;
    }
}
