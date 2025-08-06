using UnityEngine;
using UnityEngine.UI;

public class TouchRecordingAuthoring : MonoBehaviour, ITouchRecordingAuthoring
{
    [Header("Recording Settings")]
    [SerializeField] private string _saveFileName = "RecordedTouches";

    [Header("UI")]
    [SerializeField] private Button _saveButton;

    public string SaveFileName => _saveFileName;
    public Button SaveButton => _saveButton;
}
