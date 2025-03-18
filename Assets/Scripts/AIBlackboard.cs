using UnityEngine;

public class AIBlackboard : MonoBehaviour
{
    private GameObject player;
    private PlayerMove playerMove;

    public GameObject Player {
        get { 
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
            return player;
        }
    }

    public PlayerMove PlayerMove {
        get { 
            if (playerMove == null)
            {
                playerMove = Player.GetComponent<PlayerMove>();
            }
            return playerMove;
        }
    }

    public bool IsPlayerVisible()
    {
        return Player != null && PlayerMove != null && !PlayerMove.IsStealth();
    }

    public static AIBlackboard FindAIBlackboard()
    {
        GameObject blackboardObject = GameObject.FindGameObjectWithTag("AIBlackboard");

        if (blackboardObject == null)
        {
            Debug.LogError("AIBlackboard object not found");
            return null;
        }

        return blackboardObject.GetComponent<AIBlackboard>();
    }
}
