using System.Collections.Generic;
using UnityEngine;

namespace TouchReplaying
{
    public class TouchRecorder : ITouchRecorder
    {
        private readonly List<TouchLogEntry> _log = new List<TouchLogEntry>();
        private readonly ITouchInputProvider _inputProvider;
        private float _elapsedTime = 0f;
        private float _lastChangeTime = 0f;
        private bool _wasTouching = false;
        private Vector2 _lastPos;

        public TouchRecorder(ITouchInputProvider inputProvider, ITouchRecordingAuthoring authoring = null)
        {
            if (inputProvider != null)
            {
                _inputProvider = inputProvider;
                return;
            }
            if (authoring == null || authoring.EventSystem == null)
                throw new System.InvalidOperationException("No ITouchInputProvider provided and no EventSystem linked in ITouchRecordingAuthoring.");

#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
            if (authoring.EventSystem.GetComponent<UnityEngine.InputSystem.UI.InputSystemUIInputModule>() != null)
            {
                _inputProvider = new TouchInputSystem();
                return;
            }
#endif
            if (authoring.EventSystem.GetComponent<UnityEngine.EventSystems.StandaloneInputModule>() != null)
            {
                _inputProvider = new TouchInputManager();
                return;
            }
            throw new System.InvalidOperationException("EventSystem does not have a recognized Input Module.");
        }

        public IReadOnlyList<TouchLogEntry> Log => _log;

        public void Update(float deltaTime)
        {
            _elapsedTime += deltaTime;

            var touches = _inputProvider.GetTouches();
            bool currentlyTouching = touches.Count > 0;
            Vector2 currentPos = currentlyTouching ? touches[0].Position : Vector2.zero;

            if (!_wasTouching && currentlyTouching)
            {
                Record(TouchAction.Down, currentPos);
            }
            else if (_wasTouching && currentlyTouching && Vector2.Distance(currentPos, _lastPos) > 0.5f)
            {
                Record(TouchAction.Move, currentPos);
            }
            else if (_wasTouching && !currentlyTouching)
            {
                Record(TouchAction.Up, _lastPos);
            }

            _wasTouching = currentlyTouching;
            _lastPos = currentPos;
        }

        public void SimulateTouch(TouchAction action, Vector2 position, int deltaMs = 0)
        {
            _log.Add(new TouchLogEntry(action, position, deltaMs));
        }

        private void Record(TouchAction action, Vector2 pos)
        {
            int deltaMs = Mathf.RoundToInt((_elapsedTime - _lastChangeTime) * 1000f);
            _log.Add(new TouchLogEntry(action, pos, deltaMs));
            _lastChangeTime = _elapsedTime;
        }
    }
}
