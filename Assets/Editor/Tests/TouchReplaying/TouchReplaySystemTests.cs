using NUnit.Framework;
using UnityEngine;
using TouchReplaying;

public class TouchReplaySystemTests
{
    [Test]
    public void ReplaySystem_ShouldUseMockReplayer()
    {
        // Arrange
        var mockAuthoring = new MockTouchReplayAuthoring();
        var mockReplayer = new MockTouchReplayer();

        var system = new TouchReplaySystem(mockAuthoring, mockReplayer);

        // Act
        system.Update(0.1f, null);

        // Assert
        Assert.IsTrue(mockReplayer.PlayedTouches.Count >= 2);
        Assert.AreEqual(TouchAction.Down, mockReplayer.PlayedTouches[0].Action);
    }
}

public class MockTouchReplayAuthoring : ITouchReplayAuthoring
{
    public TouchLogAsset ReplayAsset { get; set; } = ScriptableObject.CreateInstance<TouchLogAsset>();

    public MockTouchReplayAuthoring()
    {
        ReplayAsset.touches.Add(new TouchLogEntry(TouchAction.Down, new Vector2(10, 10), 0));
        ReplayAsset.touches.Add(new TouchLogEntry(TouchAction.Up, new Vector2(10, 10), 100));
    }
}
