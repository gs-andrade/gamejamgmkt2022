using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed;

    private DamageSettings damageSettings;
    private ProjectileSettings projectileSettings;
    private Transform cachedTransform;
    public void Setup(DamageSettings damageSettings, ProjectileSettings projectileSettings)
    {
        if (cachedTransform == null)
            cachedTransform = transform;

        cachedTransform.position = projectileSettings.StartPosition;
        this.damageSettings = damageSettings;
        this.projectileSettings = projectileSettings;

    }

    public bool UpdateProjectileAndDisableIt()
    {
        var target = projectileSettings.Target;

        cachedTransform.position = Vector2.MoveTowards(cachedTransform.position, target.position, Speed * Time.deltaTime);

        if (target == null)
            return true;
        else if (!projectileSettings.Damagable.IsAlive())
            return true;

        if (Vector2.Distance(cachedTransform.position, target.position) < 0.25f)
        {
            DealDamage();
            return true;
        }

        return false;
    }

    private void DealDamage()
    {
       /* if (projectileSettings.ProjectileType == ProjectileType.Area)
        {
            var boxes = Physics2D.OverlapBoxAll(cachedTransform.position, Grid.Instance.NodeSize * Vector2.one, 0, Grid.Instance.EnemyLayer);

            for (int i = 0; i < boxes.Length; i++)
            {
                var explosionTarget = boxes[i].GetComponent<IDamagable>();

                if (explosionTarget != null)
                {
                    explosionTarget.TakeDamage(damageSettings);
                }
            }
        }
        else if (projectileSettings.Damagable != null)
            projectileSettings.Damagable.TakeDamage(damageSettings);*/
    }

}

[System.Serializable]
public class DamageSettings
{
    public int Damage;
    /*public IDamagable Attacker;
    public List<StatusType> Status;*/
}

public class ProjectileSettings
{
    public Transform Target;
    public IDamagable Damagable;
    public ProjectileType ProjectileType;
    public Vector2 StartPosition;
}


public enum ProjectileType
{
    Normal = 0,
    Area = 1,
    Reach = 2,

}
