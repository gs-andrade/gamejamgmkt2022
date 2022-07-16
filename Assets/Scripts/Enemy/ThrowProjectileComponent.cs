using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowProjectileComponent : MonoBehaviour, IUpdatable
{
    public GameObject ProjectilePrefab;
    public float RechargeTime;

    private float rechargeCD;

    public void UpdateObj()
    {
        if (rechargeCD > 0)
            rechargeCD -= Time.deltaTime;
        else
        {
            rechargeCD = RechargeTime;
            FireProjectile();
        }
    }

    private void FireProjectile()
    {
        var projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
        projectile.SetActive(true);

    }
}
