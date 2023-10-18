using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class OnScreenKeyboard : MonoBehaviour
{
    public static OnScreenKeyboard Instance;

    [SerializeField] private Button uppercaseButton;
    [SerializeField] private CanvasGroup keyboardCanvas;
    [SerializeField] private GameObject[] keys;

    private InputField nameField;

    private bool isNameFieldClicked = false;

    private bool isUpperCasePressed = false;

    private void Awake()
    {
        Instance = this;
    }

    public void OnInputFieldClick(InputField getField)
    {
        nameField = getField;

        isNameFieldClicked = true;
        keyboardCanvas.interactable = true;
        keyboardCanvas.blocksRaycasts = true;
        TweenKeyBoard(0f, 1f, 0.5f);
    }

    public void TweenKeyBoard(float startValue, float endValue, float duration)
    {
        DOVirtual.Float(startValue, endValue, duration, (w) => keyboardCanvas.alpha = w);
    }

    public void OnCloseKeyBoard()
    {
        TweenKeyBoard(0f, 0f, 0.5f);
        keyboardCanvas.interactable = false;
        keyboardCanvas.blocksRaycasts = false;
    }

    public void OnKeyboardButtonClick()
    {
        string buttonPressed = EventSystem.current.currentSelectedGameObject.name;

        if (buttonPressed.Equals("UPPER"))
        {
            if (isUpperCasePressed)
            {
                for (int i = 0; i < keys.Length; i++)
                {
                    keys[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.LowerCase;
                }

                uppercaseButton.GetComponent<Image>().color = Color.white;
                isUpperCasePressed = false;
            }
            else
            {

                for (int i = 0; i < keys.Length; i++)
                {
                    keys[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.UpperCase;
                }

                uppercaseButton.GetComponent<Image>().color = Color.gray;
                isUpperCasePressed = true;
            }
        }
        else
        {
            if (isUpperCasePressed)
            {
                buttonPressed = buttonPressed.ToUpper();
            }
            else
            {
                buttonPressed = buttonPressed.ToLower();
            }

            if (isNameFieldClicked)
            {
                if (buttonPressed.Equals("CANC") || buttonPressed.Equals("canc"))
                {
                    if (nameField.text.Length != 0)
                    {
                        nameField.text = nameField.text.Remove(nameField.text.Length - 1);
                    }
                }
                else
                {
                    if (buttonPressed.Equals("ENTER") || buttonPressed.Equals("enter") || buttonPressed.Equals("EMPTY") || buttonPressed.Equals("empty") || buttonPressed.Equals("UPPER") || buttonPressed.Equals("upper"))
                    {
                        TweenKeyBoard(1f, 0f, 0.5f);
                        keyboardCanvas.interactable = false;
                        keyboardCanvas.blocksRaycasts = false;
                    }
                    else
                    {
                        nameField.text += buttonPressed;
                    }
                }
            }
        }
    }
}
