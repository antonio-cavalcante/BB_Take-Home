using System.Collections;
using UnityEngine;
using UnityEngine.Pool;



public class Projectile : MonoBehaviour
{
    [SerializeField] private float timeToLive = 3f;

    public ObjectPool<Projectile> ObjectPool { get; internal set; }
    float totalTime = 1f;
    int damage = 10;

    void OnEnable()
    {
        totalTime = timeToLive;
    }

    void FixedUpdate()
    {
        totalTime -= Time.fixedDeltaTime;
        if (totalTime <= 0)
        {
            Deactivate();
        }
    }

    private void Deactivate()
    {
        if (gameObject.activeSelf == false) return;

        gameObject.SetActive(false);
        Rigidbody projectileRigidbody = GetComponent<Rigidbody>();
        projectileRigidbody.linearVelocity = new Vector3(0f, 0f, 0f);
        projectileRigidbody.angularVelocity = new Vector3(0f, 0f, 0f);

        ObjectPool.Release(this);
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            if (collision.gameObject.TryGetComponent<Health>(out var health)) 
            {
                health.TakeDamage(damage);
            }
        }
        Deactivate();
    }
}
