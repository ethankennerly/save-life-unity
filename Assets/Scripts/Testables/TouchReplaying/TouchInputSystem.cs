#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TouchReplaying
{
    /// <summary>
    /// Touch input provider using Unity's new Input System package.
    /// </summary>
    public class TouchInputSystem : ITouchInputProvider
    {
        private readonly List<TouchState> _touches = new List<TouchState>(1);

        public IReadOnlyList<TouchState> GetTouches()
        {
            _touches.Clear();

            if (Touchscreen.current != null && Touchscreen.current.touches.Count > 0)
            {
                foreach (var t in Touchscreen.current.touches)
                {
                    if (!t.press.isPressed) continue;
                    TouchAction action = t.press.wasPressedThisFrame ? TouchAction.Down :
                        (t.press.wasReleasedThisFrame ? TouchAction.Up : TouchAction.Move);
                    _touches.Add(new TouchState(t.position.ReadValue(), action, t.touchId.ReadValue()));
                }
            }
            else if (Mouse.current != null)
            {
                if (Mouse.current.leftButton.wasPressedThisFrame)
                    _touches.Add(new TouchState(Mouse.current.position.ReadValue(), TouchAction.Down));
                else if (Mouse.current.leftButton.isPressed)
                    _touches.Add(new TouchState(Mouse.current.position.ReadValue(), TouchAction.Move));
                else if (Mouse.current.leftButton.wasReleasedThisFrame)
                    _touches.Add(new TouchState(Mouse.current.position.ReadValue(), TouchAction.Up));
            }
            return _touches;
        }
    }
}
#endif
