using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonView : MonoBehaviour
{
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private Button button;
    [SerializeField] private int buttonID;
    [SerializeField] private RectTransform rectTransform;

    public Action<int> OnButtonClicked;

    void Start()
    {
        button.onClick.AddListener(OnInternalButtonClicked);
    }

    private void OnInternalButtonClicked()
    {
        OnButtonClicked?.Invoke(buttonID);
    }

    public void SetButtonText(string text)
    {
        buttonText.text = text;
    }

    public void SetButtonID(int id)
    {
        buttonID = id;
    }

    public int GetButtonID()
    {
        return buttonID;
    }

    public void SetButtonYPosition(float yPosition)
    {
        Vector2 pos = rectTransform.anchoredPosition;
        pos.y = yPosition;  
        rectTransform.anchoredPosition = pos;
    }
}
