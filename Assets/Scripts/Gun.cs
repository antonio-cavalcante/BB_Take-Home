using System;
using UnityEngine;
using UnityEngine.Pool;


public class Gun : MonoBehaviour
{
    
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private int damage = 10;

    private ObjectPool<Projectile> objectPool;

    private void Awake()
    {
        objectPool = new ObjectPool<Projectile>(CreateProjectile,null, null, OnDestroyPooledObject);
    }

    private void OnDestroyPooledObject(Projectile projectile)
    {
        Destroy(projectile.gameObject);
    }

    private Projectile CreateProjectile()
    {
        Projectile projectileInstance = Instantiate(projectilePrefab);
        projectileInstance.ObjectPool = objectPool;

        return projectileInstance;
    }

    public void Fire()
    {
        Projectile projectile = objectPool.Get();
        projectile.transform.position = transform.position + transform.forward;
        projectile.transform.rotation = transform.rotation;
        Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.linearVelocity = transform.forward * 10f;
        projectile.SetDamage(this.damage);
        projectile.gameObject.SetActive(true);
    }

    public void FireCone(int numberOfBullets, float angle)
    {
        for (int i = 0; i < numberOfBullets; i++)
        {
            Projectile projectile = objectPool.Get();
            projectile.transform.position = transform.position + transform.forward;
            projectile.transform.rotation = transform.rotation;
            projectile.transform.Rotate(Vector3.up, UnityEngine.Random.Range(-angle, angle));
            Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
            projectileRigidbody.linearVelocity = projectile.transform.forward * 10f;
            projectile.SetDamage(this.damage);
            projectile.gameObject.SetActive(true);
        }
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
