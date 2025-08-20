using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UITKEventSniffer : MonoBehaviour
{
    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        // log basic events on the whole panel
        root.RegisterCallback<PointerDownEvent>(e => Debug.Log($"PointerDown on {e.target}"), TrickleDown.TrickleDown);
        root.RegisterCallback<PointerUpEvent>(e   => Debug.Log($"PointerUp on {e.target}"),   TrickleDown.TrickleDown);
        root.RegisterCallback<ClickEvent>(e       =>
        Debug.Log($"Click on {e.target}"),
        TrickleDown.TrickleDown);

        // log keyboard/gamepad submit
        root.RegisterCallback<NavigationSubmitEvent>(e => Debug.Log($"Submit on {e.target}"), TrickleDown.TrickleDown);
    }
}