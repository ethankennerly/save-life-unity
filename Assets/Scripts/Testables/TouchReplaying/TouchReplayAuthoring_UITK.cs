using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace TouchReplaying
{
    [Serializable]
    public class TouchReplayAuthoring_UITK : ITouchReplayAuthoring
    {
        [SerializeField] private TouchLogAsset _replayAsset;
        [SerializeField] private UIDocument _document;
        private VisualElement _indicator;

        public TouchLogAsset ReplayAsset => _replayAsset;

        public UIDocument UITKDocument => _document;
        public VisualElement UITKIndicator
        {
            get
            {
                if (_indicator == null)
                {
                    _indicator = _document.rootVisualElement.Q<VisualElement>("TouchIndicator");
                }
                return _indicator;
            }
        }
    }
}
