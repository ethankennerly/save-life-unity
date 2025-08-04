using UnityEngine;
using System.Collections.Generic;

public class MockTouchReplayer : ITouchReplayer
{
    public List<TouchLogEntry> PlayedTouches { get; } = new List<TouchLogEntry>();

    public void Start()
    {
        // No-op for mock
    }

    public void Update(float deltaTime)
    {
        // Simulate reading touches deterministically
        PlayedTouches.Add(new TouchLogEntry(TouchAction.Down, new Vector2(50, 50), 0));
        PlayedTouches.Add(new TouchLogEntry(TouchAction.Up, new Vector2(50, 50), 100));
    }
}
