using TMPro;
using UnityEngine;

public class UIPanelView : MonoBehaviour
{
    [SerializeField] private GameObject scrollViewContent;
    [SerializeField] private TMP_Text stateText;
    [SerializeField] private TMP_Text damageText;

    public void AddButtonToScrollView(GameObject button)
    {
        button.transform.SetParent(scrollViewContent.transform, false);
    }

    public void SetStateText(string text)
    {
        stateText.text = text;
    }

    public void SetDamageText(string text)
    {
        damageText.text = text;
    }


}
