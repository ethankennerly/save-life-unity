using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace TouchReplaying
{
    /// <summary>
    /// UI Toolkit based touch replayer. Dispatches ClickEvents to the provided root
    /// VisualElement. Designed to be testable by injecting the root and a screen->root mapper.
    /// </summary>
    public class TouchReplayerUITK : ITouchReplayer
    {
        private readonly List<TouchLogEntry> _log;
        private readonly VisualElement _root;
        private readonly Func<Vector2, Vector2> _screenToRoot;
        private readonly VisualElement _indicator;

        private int _index;
        private float _timerMs;
        private bool _playing;

        public bool IsPlaying => _playing;
        public bool IsTouching { get; private set; }

        public TouchReplayerUITK(List<TouchLogEntry> log, VisualElement root, Func<Vector2, Vector2> screenToRoot = null, VisualElement indicator = null)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
            _root = root ?? throw new ArgumentNullException(nameof(root));
            _screenToRoot = screenToRoot ?? DefaultScreenToRoot;
            _indicator = indicator;
        }

        // Minimal default mapper: converts screen (x, y) -> root coords by flipping Y.
        // It's simple and reliable across Unity versions; integrators can inject a precise mapper.
        private Vector2 DefaultScreenToRoot(Vector2 screenPos)
        {
            return new Vector2(screenPos.x, Screen.height - screenPos.y);
        }

        public void Start()
        {
            _index = 0;
            _timerMs = 0f;
            IsTouching = false;
            _playing = _log.Count > 0;
            if (_indicator != null) _indicator.style.display = DisplayStyle.None;
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

            if (IsTouching && _indicator != null)
            {
                // place using normalized coordinates so indicator is inside root bounds
                var mapped = MapNormalizedToRoot(CurrentPositionNormalized);
                var local = new Vector2(mapped.x - _root.worldBound.x, mapped.y - _root.worldBound.y);
                _indicator.transform.position = new Vector3(local.x, local.y, 0);
            }
        }

        // Keep the last normalized position for Update logic
        private Vector2 CurrentPositionNormalized;

        private void Apply(TouchLogEntry entry)
        {
            CurrentPositionNormalized = entry.NormalizedPos;

            switch (entry.Action)
            {
                case TouchAction.Down:
                    IsTouching = true;
                    var mapped = MapNormalizedToRoot(entry.NormalizedPos);
                    ShowIndicator(entry.NormalizedPos);
                    DispatchClick(mapped);
                    break;

                case TouchAction.Move:
                    // optional: dispatch move
                    break;

                case TouchAction.Up:
                    IsTouching = false;
                    var mappedUp = MapNormalizedToRoot(entry.NormalizedPos);
                    DispatchClick(mappedUp);
                    HideIndicator();
                    break;
            }
        }

        private void ShowIndicator(Vector2 normalizedPos)
        {
            if (_indicator == null) return;
            _indicator.style.display = DisplayStyle.Flex;
            var mapped = MapNormalizedToRoot(normalizedPos);
            var local = new Vector2(mapped.x - _root.worldBound.x, mapped.y - _root.worldBound.y);
            _indicator.transform.position = new Vector3(local.x, local.y, 0);
        }

        private void HideIndicator()
        {
            if (_indicator == null) return;
            _indicator.style.display = DisplayStyle.None;
        }

        private void DispatchClick(Vector2 panelPosWorld)
        {
            // panelPosWorld is in world coordinates matching root.worldBound
            // Compute local and dispatch ClickEvent. Handlers active on elements will receive it.
            using (var click = ClickEvent.GetPooled())
            {
                click.target = _root;
                _root.SendEvent(click);
            }
        }

        private Vector2 MapNormalizedToRoot(Vector2 normalized)
        {
            // Map normalized (0..1) where Y=0 bottom to worldBound coordinates
            var wb = _root.worldBound;
            var nx = normalized.x;
            var ny = 1f - normalized.y;

            // normalized Y in TouchLogEntry is screen fraction from bottom (0..1),
            // but worldBound origin's Y is top-left, so convert accordingly.
            // worldBound y increases downward in UI Toolkit coordinates.
            var mappedX = wb.x + nx * wb.width;
            var mappedY = wb.y + ny * wb.height;
            return new Vector2(mappedX, mappedY);
        }
    }
}
