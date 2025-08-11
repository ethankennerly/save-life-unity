using System.Collections.Generic;
using UnityEngine;

namespace TouchReplaying
{
    /// <summary>
    /// Touch input provider using Unity's Input Manager (legacy Input class).
    /// </summary>
    public class TouchInputManager : ITouchInputProvider
    {
        private readonly List<TouchState> _touches = new List<TouchState>(1);

        public IReadOnlyList<TouchState> GetTouches()
        {
            _touches.Clear();

#if UNITY_ANDROID || UNITY_IOS
            for (int i = 0; i < Input.touchCount; i++)
            {
                var t = Input.GetTouch(i);
                TouchAction action = t.phase switch
                {
                    TouchPhase.Began => TouchAction.Down,
                    TouchPhase.Moved => TouchAction.Move,
                    TouchPhase.Stationary => TouchAction.Move,
                    TouchPhase.Ended => TouchAction.Up,
                    TouchPhase.Canceled => TouchAction.Up,
                    _ => TouchAction.Move
                };
                _touches.Add(new TouchState(t.position, action, t.fingerId));
            }
#else
            // Simulate single touch with mouse for editor/desktop
            if (Input.GetMouseButtonDown(0))
                _touches.Add(new TouchState(Input.mousePosition, TouchAction.Down));
            else if (Input.GetMouseButton(0))
                _touches.Add(new TouchState(Input.mousePosition, TouchAction.Move));
            else if (Input.GetMouseButtonUp(0))
                _touches.Add(new TouchState(Input.mousePosition, TouchAction.Up));
#endif
            return _touches;
        }
    }
}
