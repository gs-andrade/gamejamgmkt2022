using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;
    public Projectile ProjectilePrefab;

    private Pool<Projectile> poolProjectile;
    public void Setup()
    {
        if (Instance == null)
            Instance = this;

        if (Instance != this)
            Destroy(this);

        poolProjectile = new Pool<Projectile>(10, ProjectilePrefab, transform.GetChild(0));
    }

    public Projectile GetProjectileInstance()
    {
        return poolProjectile.GetInstance();
    }

    public void ReturnToPool(Projectile instance) 
    {
        instance.transform.position = new Vector2(-100, -100);
        poolProjectile.AddToPool(instance);
    }

    
}



