using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProjectileController
{
    private static List<Projectile> activeProjectiles = new List<Projectile>();
    public static void ActiveNewProjectile(DamageSettings damageSettings, ProjectileSettings projectileSettings)
    {
        var projectile = PoolManager.Instance.GetProjectileInstance();
        projectile.Setup(damageSettings, projectileSettings);
        activeProjectiles.Add(projectile);
    }

    public static void UpdateProjectiles()
    {
        for (int i = 0; i < activeProjectiles.Count; i++)
        {
            if (activeProjectiles[i].UpdateProjectileAndDisableIt())
            {
                PoolManager.Instance.ReturnToPool(activeProjectiles[i]);
                activeProjectiles.RemoveAt(i);
                i--;
            }
        }
    }

    public static void RestartBattle()
    {
        for (int i = 0; i < activeProjectiles.Count; i++)
        {
            PoolManager.Instance.ReturnToPool(activeProjectiles[i]);
        }

        activeProjectiles.Clear();
    }
}
