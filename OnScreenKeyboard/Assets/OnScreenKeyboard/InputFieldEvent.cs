using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldEvent : MonoBehaviour, ISelectHandler
{
    private InputField GetInputField;
    public void OnSelect(BaseEventData eventData)
    {
        GetInputField = GetComponent<InputField>();
        OnScreenKeyboard.Instance.OnInputFieldClick(GetInputField);
    }
}