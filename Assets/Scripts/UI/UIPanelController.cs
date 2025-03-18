using System;
using UnityEngine;

public class UIPanelController : MonoBehaviour
{
    [SerializeField] private UIPanelView panelView;
    [SerializeField] private GameObject buttonPrefab;

    private AIBlackboard blackboard;
    private PlayerMove playerMove;

    void Start()
    {
        blackboard = AIBlackboard.FindAIBlackboard();
        playerMove = blackboard != null ? blackboard.PlayerMove : null;

        if (playerMove == null)
        {
            Debug.LogError("PlayerMove not found");
            return;
        }

        for (int i = 0; i < playerMove.GetStateCount(); i++)
        {
            CreateButton(playerMove.GetStateName(i), i);
        }

        playerMove.OnStateChange += OnStateChange;
    }

    private void OnStateChange(PlayerStateScriptableObject state)
    {
        panelView.SetStateText("Mode: " + state.StateName);
        panelView.SetDamageText("Damage: " + state.AttackDamage.ToString());
    }

    void CreateButton(string text, int id)
    {
        GameObject button = Instantiate(buttonPrefab);
        UIButtonView buttonView = button.GetComponent<UIButtonView>();
        buttonView.OnButtonClicked += OnButtonClicked;
        buttonView.SetButtonText(text);
        buttonView.SetButtonID(id);
        panelView.AddButtonToScrollView(button);
        buttonView.SetButtonYPosition(-id * 35 - 35);
    }

    private void OnButtonClicked(int id)
    {
        playerMove.ChangeState(id);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
