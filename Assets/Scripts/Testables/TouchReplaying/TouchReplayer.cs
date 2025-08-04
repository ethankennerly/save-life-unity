using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class TouchReplayer
{
    private readonly List<TouchLogEntry> log;
    private int index = 0;
    private float timer = 0f;
    private bool active = false;

    public bool IsPlaying => active;
    public Vector2 CurrentPosition { get; private set; }
    public bool IsTouching { get; private set; }

    private TouchAction lastAction;

    // ✅ Touch Indicator
    private Image touchIndicatorInstance;
    private readonly Canvas targetCanvas;
    private readonly Sprite indicatorSprite;
    private readonly Color indicatorColor;

    public TouchReplayer(
        List<TouchLogEntry> log, 
        Canvas uiCanvas = null, 
        Sprite indicatorSprite = null, 
        Color? indicatorColor = null)
    {
        this.log = log;
        this.targetCanvas = uiCanvas;
        this.indicatorSprite = indicatorSprite;
        this.indicatorColor = indicatorColor ?? new Color(0f, 1f, 0f, 0.5f); // Default translucent green
    }

    public void Start()
    {
        index = 0;
        timer = 0f;
        IsTouching = false;
        active = log != null && log.Count > 0;
    }

    public void Update(float deltaTime)
    {
        if (!active || index >= log.Count) return;

        timer += deltaTime * 1000f; // ms

        var entry = log[index];
        if (timer >= entry.DeltaMs)
        {
            Apply(entry);
            timer = 0f;
            index++;
            if (index >= log.Count)
                active = false;
        }

        // ✅ Move indicator if touch is down
        if (IsTouching && touchIndicatorInstance != null)
        {
            Vector2 uiPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                targetCanvas.transform as RectTransform,
                CurrentPosition,
                targetCanvas.worldCamera,
                out uiPos
            );
            touchIndicatorInstance.rectTransform.anchoredPosition = uiPos;
        }
    }

    private void Apply(TouchLogEntry entry)
    {
        lastAction = entry.Action;
        CurrentPosition = entry.GetScreenPos();

        switch (entry.Action)
        {
            case TouchAction.Down:
                IsTouching = true;
                ShowTouchIndicator(CurrentPosition);
                SimulatePointerDown(CurrentPosition);
                break;

            case TouchAction.Move:
                // Optional drag simulation
                break;

            case TouchAction.Up:
                IsTouching = false;
                SimulatePointerUpAndClick(CurrentPosition);
                HideTouchIndicator();
                break;
        }
    }

    private void SimulatePointerDown(Vector2 screenPos)
    {
        var target = ResolveTarget(screenPos);
        if (target != null)
        {
            var pointerData = new PointerEventData(EventSystem.current) { position = screenPos };
            ExecuteEvents.Execute(target, pointerData, ExecuteEvents.pointerDownHandler);
        }
    }

    private void SimulatePointerUpAndClick(Vector2 screenPos)
    {
        var target = ResolveTarget(screenPos);
        if (target != null)
        {
            var pointerData = new PointerEventData(EventSystem.current) { position = screenPos };
            ExecuteEvents.Execute(target, pointerData, ExecuteEvents.pointerUpHandler);

            // ✅ Click only if it has a click handler
            if (ExecuteEvents.GetEventHandler<IPointerClickHandler>(target) != null)
            {
                ExecuteEvents.Execute(target, pointerData, ExecuteEvents.pointerClickHandler);
            }
        }
    }

    /// <summary>
    /// ✅ Finds the first topmost UI element and its clickable parent if available.
    /// Stops at first hit to respect overlays like panels or raw images.
    /// </summary>
    private GameObject ResolveTarget(Vector2 screenPos)
    {
        var pointerData = new PointerEventData(EventSystem.current) { position = screenPos };
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        if (results.Count == 0)
            return null;

        // ✅ Use only the first (topmost) result
        var firstHit = results[0].gameObject;

        // Find nearest clickable parent (like Unity does for child text inside a button)
        var clickable = ExecuteEvents.GetEventHandler<IPointerClickHandler>(firstHit);
        return clickable ?? firstHit; // Could be a blocker (panel or raw image)
    }

    // ✅ Touch Indicator methods
    private void ShowTouchIndicator(Vector2 screenPos)
    {
        if (targetCanvas == null || indicatorSprite == null)
            return;

        if (touchIndicatorInstance == null)
        {
            var go = new GameObject("TouchIndicator", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            go.transform.SetParent(targetCanvas.transform, false);

            touchIndicatorInstance = go.GetComponent<Image>();
            touchIndicatorInstance.sprite = indicatorSprite;
            touchIndicatorInstance.color = indicatorColor;
            touchIndicatorInstance.raycastTarget = false;

            touchIndicatorInstance.rectTransform.sizeDelta = new Vector2(50f, 50f);
        }

        touchIndicatorInstance.gameObject.SetActive(true);

        Vector2 uiPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            targetCanvas.transform as RectTransform,
            screenPos,
            targetCanvas.worldCamera,
            out uiPos
        );

        touchIndicatorInstance.rectTransform.anchoredPosition = uiPos;
    }

    private void HideTouchIndicator()
    {
        if (touchIndicatorInstance != null)
            touchIndicatorInstance.gameObject.SetActive(false);
    }
}
