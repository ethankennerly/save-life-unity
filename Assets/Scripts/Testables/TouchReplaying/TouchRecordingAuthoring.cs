using UnityEngine;
using UnityEngine.UI;

using UnityEngine.EventSystems;

public class TouchRecordingAuthoring : MonoBehaviour, ITouchRecordingAuthoring
{
    [Header("Recording Settings")]
    [SerializeField] private string _saveFileName = "RecordedTouches";

    [Header("UI")]
    [SerializeField] private Button _saveButton;

    [Header("Event System")]
    [SerializeField] private EventSystem _eventSystem;

    public string SaveFileName => _saveFileName;
    public Button SaveButton => _saveButton;
    public EventSystem EventSystem => _eventSystem;
}
