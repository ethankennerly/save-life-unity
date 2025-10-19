using UnityEngine;

namespace TouchReplaying
{
    // Minimal authoring contract: implementations own their UI dependencies.
    public interface ITouchReplayAuthoring
    {
        TouchLogAsset ReplayAsset { get; }
    }
}
