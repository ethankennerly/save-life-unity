using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TouchReplaying
{
    /// <summary>
    /// uGUI-based touch replayer. Replays touch log entries using Unity's EventSystem
    /// and creates a simple touch indicator (Image) on a provided Canvas.
    /// </summary>
    public class TouchReplayer : ITouchReplayer
    {
        private readonly List<TouchLogEntry> _log;
        private readonly Canvas _targetCanvas;
        private readonly Sprite _indicatorSprite;
        private readonly Color _indicatorColor;

        private Image _indicatorInstance;
        private int _index;
        private float _timerMs;
        private bool _playing;

        public bool IsPlaying => _playing;
        public Vector2 CurrentPosition { get; private set; }
        public bool IsTouching { get; private set; }

        public TouchReplayer(List<TouchLogEntry> log, Canvas uiCanvas = null, Sprite indicatorSprite = null, Color? indicatorColor = null)
        {
            _log = log ?? new List<TouchLogEntry>();
            _targetCanvas = uiCanvas;
            _indicatorSprite = indicatorSprite;
            _indicatorColor = indicatorColor ?? new Color(0f, 1f, 0f, 0.5f);
        }

        public void Start()
        {
            _index = 0;
            _timerMs = 0f;
            IsTouching = false;
            _playing = _log.Count > 0;
            if (_indicatorInstance != null) _indicatorInstance.gameObject.SetActive(false);
        }

        public void Update(float deltaSeconds)
        {
            if (!_playing || _index >= _log.Count) return;

            _timerMs += deltaSeconds * 1000f;

            var entry = _log[_index];
            if (_timerMs >= entry.DeltaMs)
            {
                Apply(entry);
                _timerMs = 0f;
                _index++;
                if (_index >= _log.Count) _playing = false;
            }

            if (IsTouching && _indicatorInstance != null && _targetCanvas != null)
            {
                Vector2 uiPos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _targetCanvas.transform as RectTransform,
                    CurrentPosition,
                    _targetCanvas.worldCamera,
                    out uiPos
                );
                _indicatorInstance.rectTransform.anchoredPosition = uiPos;
            }
        }

        private void Apply(TouchLogEntry entry)
        {
            CurrentPosition = entry.GetScreenPos();

            switch (entry.Action)
            {
                case TouchAction.Down:
                    IsTouching = true;
                    ShowIndicator(CurrentPosition);
                    SimulatePointerDown(CurrentPosition);
                    break;

                case TouchAction.Move:
                    // optional: simulate drag
                    break;

                case TouchAction.Up:
                    IsTouching = false;
                    SimulatePointerUpAndClick(CurrentPosition);
                    HideIndicator();
                    break;
            }
        }

        private void ShowIndicator(Vector2 screenPos)
        {
            if (_targetCanvas == null || _indicatorSprite == null)
                return;

            if (_indicatorInstance == null)
            {
                var go = new GameObject("TouchIndicator", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
                go.transform.SetParent(_targetCanvas.transform, false);

                _indicatorInstance = go.GetComponent<Image>();
                _indicatorInstance.sprite = _indicatorSprite;
                _indicatorInstance.color = _indicatorColor;
                _indicatorInstance.raycastTarget = false;
                _indicatorInstance.rectTransform.sizeDelta = new Vector2(50f, 50f);
            }

            _indicatorInstance.gameObject.SetActive(true);

            Vector2 uiPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _targetCanvas.transform as RectTransform,
                screenPos,
                _targetCanvas.worldCamera,
                out uiPos
            );

            _indicatorInstance.rectTransform.anchoredPosition = uiPos;
        }

        private void HideIndicator()
        {
            if (_indicatorInstance != null)
                _indicatorInstance.gameObject.SetActive(false);
        }

        private void SimulatePointerDown(Vector2 screenPos)
        {
            var target = ResolveTarget(screenPos);
            if (target == null) return;

            var pointerData = new PointerEventData(EventSystem.current) { position = screenPos };
            ExecuteEvents.Execute(target, pointerData, ExecuteEvents.pointerDownHandler);
        }

        private void SimulatePointerUpAndClick(Vector2 screenPos)
        {
            var target = ResolveTarget(screenPos);
            if (target == null) return;

            var pointerData = new PointerEventData(EventSystem.current) { position = screenPos };
            ExecuteEvents.Execute(target, pointerData, ExecuteEvents.pointerUpHandler);

            if (ExecuteEvents.GetEventHandler<IPointerClickHandler>(target) != null)
            {
                ExecuteEvents.Execute(target, pointerData, ExecuteEvents.pointerClickHandler);
            }
        }

        private GameObject ResolveTarget(Vector2 screenPos)
        {
            var pointerData = new PointerEventData(EventSystem.current) { position = screenPos };
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            if (results.Count == 0) return null;

            var firstHit = results[0].gameObject;
            var clickable = ExecuteEvents.GetEventHandler<IPointerClickHandler>(firstHit);
            return clickable ?? firstHit;
        }
    }
}
