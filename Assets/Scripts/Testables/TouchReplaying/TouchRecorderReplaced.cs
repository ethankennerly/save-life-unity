using System.Collections.Generic;
using UnityEngine;



using TouchReplaying;
public class TouchRecorder : ITouchRecorder
{
    private readonly List<TouchLogEntry> _log = new List<TouchLogEntry>();
    private readonly ITouchInputProvider _inputProvider;
    private float _elapsedTime = 0f;
    private float _lastChangeTime = 0f;
    private bool _wasTouching = false;
    private Vector2 _lastPos;

    public TouchRecorder(ITouchInputProvider inputProvider)
    {
        _inputProvider = inputProvider;
    }

    public IReadOnlyList<TouchLogEntry> Log => _log;

    public void Update(float deltaTime)
    {
        _elapsedTime += deltaTime;

        var touches = _inputProvider.GetTouches();
        bool currentlyTouching = touches.Count > 0;
        Vector2 currentPos = currentlyTouching ? touches[0].Position : Vector2.zero;

        if (!_wasTouching && currentlyTouching)
        {
            Record(TouchAction.Down, currentPos);
        }
        else if (_wasTouching && currentlyTouching && Vector2.Distance(currentPos, _lastPos) > 0.5f)
        {
            Record(TouchAction.Move, currentPos);
        }
        else if (_wasTouching && !currentlyTouching)
        {
            Record(TouchAction.Up, _lastPos);
        }

        _wasTouching = currentlyTouching;
        _lastPos = currentPos;
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
