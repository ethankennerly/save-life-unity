using UnityEngine;

public interface ITouchReplayAuthoring
{
    TouchLogAsset ReplayAsset { get; }
    Canvas TargetCanvas { get; }
    Sprite IndicatorSprite { get; }
    Color IndicatorColor { get; }
}
