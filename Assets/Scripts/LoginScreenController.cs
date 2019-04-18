using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoginScreenController : MonoBehaviour
{
    public TMP_InputField LoginInput;
    public TMP_InputField PasswordInput;
    public Button EnterButton;

    public EventSystem eventSystem;

    public RectTransform PopUp;

    public void Start()
    {
        LoginInput.onValueChanged.AddListener(FinishEditLogin);
        PasswordInput.onValueChanged.AddListener(FinishEditPassword);

        EnterButton.onClick.AddListener(ButtonClicked);

        UpdateEnterButtonStatus();
    }

    private void FinishEditLogin(string value)
    {
        UpdateEnterButtonStatus();
    }

    private void FinishEditPassword(string value)
    {
        UpdateEnterButtonStatus();
    }

    private void ButtonClicked()
    {
        PopUp.DOShakeScale(0.5f, 1, 10, 90, true);
    }

    private void UpdateEnterButtonStatus()
    {
        EnterButton.interactable = (LoginInput.text != string.Empty && PasswordInput.text != string.Empty);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            var selected = eventSystem.currentSelectedGameObject;
            if (selected == null)
            {
                selected = LoginInput.gameObject;
            }
            Selectable next = selected.GetComponent<Selectable>().FindSelectableOnDown();

            if (next == null)
            {
                next = LoginInput;
            }

            var inputfield = next.GetComponent<TMP_InputField>();
            if (inputfield != null)
                inputfield.OnPointerClick(
                    new PointerEventData(eventSystem)); //if it's an input field, also set the text caret

            eventSystem.SetSelectedGameObject(next.gameObject, new BaseEventData(eventSystem));
        }
    }
}