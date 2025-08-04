using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TouchRecordingManager : MonoBehaviour
{
    [Header("Replay Settings")]
    public bool isReplayMode = false;
    public TouchLogAsset replayAsset;

    [SerializeField]
    private Canvas targetCanvas;
    [SerializeField]
    private Sprite indicatorSprite;
    [SerializeField]
    private Color indicatorColor;

    [Header("Record Settings")]
    public string saveFileName = "RecordedTouches";

    [Header("UI")]
    public Button saveButton;

    // Internal
    private TouchRecorder recorder;
    private TouchReplayer replayer;
    private int sessionSeed;

    void Awake()
    {
        if (isReplayMode)
        {
            InitializeReplay();
        }
        else
        {
            InitializeRecording();
        }

        if (saveButton != null)
        {
            saveButton.onClick.AddListener(SaveLogRuntime);
        }
    }

    private void InitializeRecording()
    {
        // ✅ Set and store random seed
        sessionSeed = System.Environment.TickCount;
        Random.InitState(sessionSeed);

        Debug.Log($"[TouchRecordingManager] Recording session seed: {sessionSeed}");
        recorder = new TouchRecorder();
    }

    private void InitializeReplay()
    {
        if (replayAsset != null && replayAsset.touches.Count > 0)
        {
            // ✅ Reinitialize Unity's Random with recorded seed
            Random.InitState(replayAsset.randomSeed);

            Debug.Log($"[TouchRecordingManager] Replaying with seed: {replayAsset.randomSeed}");
            replayer = new TouchReplayer(replayAsset.touches, targetCanvas, indicatorSprite, indicatorColor);
            replayer.Start();
        }
        else
        {
            Debug.LogWarning("Replay mode enabled but TouchLogAsset is missing or empty.");
        }
    }

    void Update()
    {
        if (isReplayMode)
        {
            replayer?.Update(Time.deltaTime);
        }
        else
        {
            recorder?.Update();
        }
    }

    public void SaveLogRuntime()
    {
#if UNITY_EDITOR
        if (recorder == null || recorder.Log == null || recorder.Log.Count == 0)
        {
            Debug.LogWarning("No touch data recorded to save.");
            return;
        }

        TouchLogAsset asset = ScriptableObject.CreateInstance<TouchLogAsset>();
        asset.touches = new List<TouchLogEntry>(recorder.Log);

        // ✅ Save session seed
        asset.randomSeed = sessionSeed;

        string folderPath = "Assets/TouchLogs";
        if (!System.IO.Directory.Exists(folderPath))
            System.IO.Directory.CreateDirectory(folderPath);

        string path = $"{folderPath}/{saveFileName}.asset";
        AssetDatabase.CreateAsset(asset, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"✅ Touch log and seed saved to: {path}");
#else
        Debug.LogWarning("Touch log saving is only available in the Unity Editor.");
#endif
    }
}
