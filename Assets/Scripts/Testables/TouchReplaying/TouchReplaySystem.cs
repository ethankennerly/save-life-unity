using EthanKennerly.PoorLife;
using System.Collections.Generic;
using UnityEngine;

public class TouchReplaySystem : ISystem
{
    private readonly ITouchReplayAuthoring _authoring;
    private readonly ITouchReplayer _replayer;

    public TouchReplaySystem(ITouchReplayAuthoring authoring, ITouchReplayer replayer = null)
    {
        _authoring = authoring;

        var replayAsset = _authoring.ReplayAsset;
        if (replayAsset != null &&
            replayAsset.touches != null &&
            replayAsset.touches.Count > 0)
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

    public void Update(float deltaTime, List<IComponent> commands)
    {
        _replayer?.Update(deltaTime);
    }
}
