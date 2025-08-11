
using EthanKennerly.SaveLife;
using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TouchReplaying
{
    public class TouchRecordingSystem : ISystem
    {
        private readonly ITouchRecordingAuthoring _authoring;
        private readonly ITouchRecorder _recorder;
        private readonly int _sessionSeed;

        public TouchRecordingSystem(ITouchRecordingAuthoring authoring, ITouchInputProvider inputProvider = null, ITouchRecorder recorder = null)
        {
            _authoring = authoring;
            _sessionSeed = System.Environment.TickCount;

            Random.InitState(_sessionSeed);

            _recorder = recorder ?? new TouchRecorder(inputProvider, authoring);

            _authoring.SaveButton.onClick.AddListener(SaveLog);
        }

        public void Update(float deltaTime, List<IComponent> _)
        {
            _recorder.Update(deltaTime);
        }

        private void SaveLog()
        {
#if UNITY_EDITOR
            if (_recorder.Log == null || _recorder.Log.Count == 0)
            {
                Debug.LogWarning("No touch data recorded to save.");
                return;
            }

            var asset = ScriptableObject.CreateInstance<TouchLogAsset>();
            asset.randomSeed = _sessionSeed;
            asset.touches = new List<TouchLogEntry>(_recorder.Log);

            string folderPath = "Assets/TouchLogs";
            if (!System.IO.Directory.Exists(folderPath))
                System.IO.Directory.CreateDirectory(folderPath);

            string path = $"{folderPath}/{_authoring.SaveFileName}.asset";
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"✅ Touch log saved: {path}");
#endif
        }
    }
}
