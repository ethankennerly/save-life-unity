using System.Collections.Generic;
using UnityEngine;

public interface ITouchRecorder
{
    IReadOnlyList<TouchLogEntry> Log { get; }
    void Update(float deltaTime);
    void SimulateTouch(TouchAction action, Vector2 position, int deltaMs = 0);
}
