using UnityEngine.UI;

using UnityEngine.EventSystems;

public interface ITouchRecordingAuthoring
{
    string SaveFileName { get; }
    Button SaveButton { get; }
    EventSystem EventSystem { get; }
}
