using UnityEngine;

public class TouchReplayAuthoring : MonoBehaviour, ITouchReplayAuthoring
{
    [Header("Replay Data")]
    [SerializeField] private TouchLogAsset replayAsset;

    [Header("Visual Indicator")]
    [SerializeField] private Canvas targetCanvas;
    [SerializeField] private Sprite indicatorSprite;
    [SerializeField] private Color indicatorColor = new Color(0f, 1f, 0f, 0.5f);

    public TouchLogAsset ReplayAsset => replayAsset;
    public Canvas TargetCanvas => targetCanvas;
    public Sprite IndicatorSprite => indicatorSprite;
    public Color IndicatorColor => indicatorColor;

    private TouchReplaySystem _system;

    private void Awake()
    {
        _system = new TouchReplaySystem(this);
    }

    private void Update()
    {
        _system?.Update(Time.deltaTime);
    }
}
