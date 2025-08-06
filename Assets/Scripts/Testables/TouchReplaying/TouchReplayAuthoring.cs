using UnityEngine;

public class TouchReplayAuthoring : MonoBehaviour, ITouchReplayAuthoring
{
    [Header("Replay Data")]
    [SerializeField] private TouchLogAsset _replayAsset;

    [Header("Visual Indicator")]
    [SerializeField] private Canvas _targetCanvas;
    [SerializeField] private Sprite _indicatorSprite;
    [SerializeField] private Color _indicatorColor = new Color(0f, 1f, 0f, 0.5f);

    public TouchLogAsset ReplayAsset => _replayAsset;
    public Canvas TargetCanvas => _targetCanvas;
    public Sprite IndicatorSprite => _indicatorSprite;
    public Color IndicatorColor => _indicatorColor;
}
