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
    public class TouchReplayerUITK
    {
        private readonly List<TouchLogEntry> _log;
        private readonly VisualElement _root;
        private readonly Func<Vector2, Vector2> _screenToRoot;
        private readonly VisualElement _indicator;

        private int _index;
        private float _timerMs;
        private bool _playing;

        public bool IsPlaying => _playing;
        public Vector2 CurrentPosition { get; private set; }
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
                var rootPos = _screenToRoot(CurrentPosition);
                _indicator.transform.position = new Vector3(rootPos.x, rootPos.y, 0);
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
                    DispatchClick(CurrentPosition);
                    break;

                case TouchAction.Move:
                    // optional: dispatch move
                    break;

                case TouchAction.Up:
                    IsTouching = false;
                    DispatchClick(CurrentPosition);
                    HideIndicator();
                    break;
            }
        }

        private void ShowIndicator(Vector2 screenPos)
        {
            if (_indicator == null) return;
            _indicator.style.display = DisplayStyle.Flex;
            var rootPos = _screenToRoot(screenPos);
            _indicator.transform.position = new Vector3(rootPos.x, rootPos.y, 0);
        }

        private void HideIndicator()
        {
            if (_indicator == null) return;
            _indicator.style.display = DisplayStyle.None;
        }

        private void DispatchClick(Vector2 screenPos)
        {
            using (var click = ClickEvent.GetPooled())
            {
                click.target = _root;
                // optionally set localPosition/related fields if needed by handlers
                _root.SendEvent(click);
            }
        }
    }
}
