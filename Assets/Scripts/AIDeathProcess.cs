using UnityEngine;

public class AIDeathProcess : MonoBehaviour
{
    [SerializeField] float Delay = 2f;

    public void DestroyOnDeath(GameObject gameObject)
    {
        Destroy(gameObject, Delay);
    }
}
