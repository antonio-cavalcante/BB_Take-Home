using UnityEngine;
using System.Collections;

public class AISpawner : MonoBehaviour
{
    [SerializeField] private GameObject aiPrefab;
    [SerializeField] private float minDelay = 5f;
    [SerializeField] private float maxDelay = 8f;

    private Coroutine spawnCoroutine;

    private void OnEnable()
    {
        spawnCoroutine = StartCoroutine(SpawnAICoroutine());
    }

    private void OnDisable()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnAICoroutine()
    {
        while (true)
        {
            if (aiPrefab != null)
            {
                //check if position is free
                Collider[] colliders = Physics.OverlapSphere(transform.position, 1f, LayerMask.GetMask("Enemy"));
                if (colliders.Length == 0) {
                    Instantiate(aiPrefab, transform.position, Quaternion.identity);
                }
            }
            
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        }
    }
}
