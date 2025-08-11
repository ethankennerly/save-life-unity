using UnityEngine;
using System.Collections.Generic;

namespace TouchReplaying
{
    /// <summary>
    /// Abstracts touch input for recording and gameplay, enabling support for multiple input backends.
    /// </summary>
    public interface ITouchInputProvider
    {
        /// <summary>
        /// Returns the current active touches (or mouse as a single touch if on desktop).
        /// </summary>
        IReadOnlyList<TouchState> GetTouches();
    }

    /// <summary>
    /// Represents a single touch or mouse input event.
    /// </summary>
    public struct TouchState
    {
        public Vector2 Position;
        public TouchAction Action;
        public int FingerId;

        public TouchState(Vector2 position, TouchAction action, int fingerId = 0)
        {
            Position = position;
            Action = action;
            FingerId = fingerId;
        }
    }
}
