using UnityEngine;
using UnityEngine.UI;

public class TouchRecordingAuthoring : MonoBehaviour, ITouchRecordingAuthoring
{
    [Header("Recording Settings")]
    [SerializeField] private string saveFileName = "RecordedTouches";

    [Header("UI")]
    [SerializeField] private Button saveButton;

    public string SaveFileName => saveFileName;
    public Button SaveButton => saveButton;
}
