using EthanKennerly.SaveLife;
using System.Collections.Generic;
using UnityEngine;
using TouchReplaying;
using UnityEngine.UIElements;

public class TouchReplaySystem : ISystem
{
    private readonly ITouchReplayAuthoring _authoring;
    private readonly ITouchReplayer _replayer;

    public TouchReplaySystem(ITouchReplayAuthoring authoring, ITouchReplayer replayer = null)
    {
        if (authoring == null)
        {
            Debug.LogWarning("TouchReplaySystem: Authoring is null.");
            return;
        }

        _authoring = authoring;
        var replayAsset = _authoring.ReplayAsset;
        if (replayAsset == null || replayAsset.touches == null || replayAsset.touches.Count == 0)
        {
            Debug.LogWarning("TouchReplaySystem: No replay asset or touches provided.");
            return;
        }

        if (replayer == null)
        {
            // Prefer UITK authoring if the concrete authoring type is present
            if (authoring is TouchReplayAuthoring_UITK uitkAuthoring && uitkAuthoring.UITKDocument != null)
            {
                var root = uitkAuthoring.UITKDocument.rootVisualElement;
                replayer = new TouchReplayerUITK(replayAsset.touches, root, null, uitkAuthoring.UITKIndicator);
            }
            else if (authoring is TouchReplaying.TouchReplayAuthoring guiAuthoring)
            {
                replayer = new TouchReplayerConcrete(
                    replayAsset.touches,
                    guiAuthoring.TargetCanvas,
                    guiAuthoring.IndicatorSprite,
                    guiAuthoring.IndicatorColor
                );
            }
            else
            {
                Debug.LogWarning("TouchReplaySystem: Unknown authoring type; cannot auto-select replayer.");
                return;
            }
        }

        _replayer = replayer;
        _replayer.Start();
    }

    public void Update(float deltaTime, List<IComponent> _)
    {
        if (_replayer == null) return;
        _replayer.Update(deltaTime);
    }
}
