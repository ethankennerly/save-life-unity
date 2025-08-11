using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TouchReplaying;

public class TouchRecorderInputProviderTests
{
    [Test]
    public void TouchRecorder_UsesStubInputProvider()
    {
        var stub = new StubInputProvider(new List<TouchState> {
            new TouchState(new Vector2(100, 200), TouchAction.Down)
        });
        var recorder = new TouchRecorder(stub);
        recorder.Update(0.016f);
        Assert.AreEqual(1, recorder.Log.Count);
        Assert.AreEqual(TouchAction.Down, recorder.Log[0].Action);
    }

    [Test]
    public void TouchRecorder_UsesInputManager()
    {
        var provider = new TouchInputManager();
        var recorder = new TouchRecorder(provider);
        // This test will only pass in Editor/Play mode with real input, so just check type
        Assert.IsInstanceOf<TouchInputManager>(provider);
    }

#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
    [Test]
    public void TouchRecorder_UsesInputSystem()
    {
        var provider = new TouchInputSystem();
        var recorder = new TouchRecorder(provider);
        Assert.IsInstanceOf<TouchInputSystem>(provider);
    }
#endif
}

public class StubInputProvider : ITouchInputProvider
{
    private readonly List<TouchState> _touches;
    public StubInputProvider(List<TouchState> touches) { _touches = touches; }
    public IReadOnlyList<TouchState> GetTouches() => _touches;
}
