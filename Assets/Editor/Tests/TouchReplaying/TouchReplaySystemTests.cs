using NUnit.Framework;
using UnityEngine;

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
        system.Update(0.1f);

        // Assert
        Assert.IsTrue(mockReplayer.PlayedTouches.Count >= 2);
        Assert.AreEqual(TouchAction.Down, mockReplayer.PlayedTouches[0].Action);
    }
}

public class MockTouchReplayAuthoring : ITouchReplayAuthoring
{
    public TouchLogAsset ReplayAsset { get; set; } = ScriptableObject.CreateInstance<TouchLogAsset>();
    public Canvas TargetCanvas { get; set; }
    public Sprite IndicatorSprite { get; set; }
    public Color IndicatorColor { get; set; } = Color.green;

    public MockTouchReplayAuthoring()
    {
        ReplayAsset.touches.Add(new TouchLogEntry(TouchAction.Down, new Vector2(10, 10), 0));
    }
}
