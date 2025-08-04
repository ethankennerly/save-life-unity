using UnityEngine;

public class TouchReplaySystem
{
    private readonly ITouchReplayAuthoring _authoring;
    private readonly ITouchReplayer _replayer;

    public TouchReplaySystem(ITouchReplayAuthoring authoring, ITouchReplayer replayer = null)
    {
        _authoring = authoring;

        if (_authoring.ReplayAsset != null && _authoring.ReplayAsset.touches.Count > 0)
        {
            // Use provided mock or real replayer
            _replayer = replayer ?? new TouchReplayerConcrete(
                _authoring.ReplayAsset.touches,
                _authoring.TargetCanvas,
                _authoring.IndicatorSprite,
                _authoring.IndicatorColor
            );

            _replayer.Start();
        }
        else
        {
            Debug.LogWarning("TouchReplaySystem: No replay asset or touches provided.");
        }
    }

    public void Update(float deltaTime)
    {
        _replayer?.Update(deltaTime);
    }
}
