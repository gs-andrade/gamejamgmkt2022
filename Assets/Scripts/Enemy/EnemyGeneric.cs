using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyGeneric : MonoBehaviour, IUpdatable, IResetable, IDamagable
{
    [Header("Generic Enemy Attributes")]
    public int LifeMax = 6;

    protected int lifeCurrent;

    protected float disableTime;

    public abstract void SetupOnStartLevel();

    public abstract void ResetObject();

    public abstract void UpdateObj();

    public virtual bool DealDamage(CharacterInstance character)
    {
        if (disableTime <= 0 && !character.IsVunerable())
        {
            character.TakeDamage(transform.position);
            return true;
        }

        return false;
    }

    public abstract void TakeDamage(Vector2 damagerDirection);

    public bool IsAlive()
    {
        return lifeCurrent > 0;
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public Transform GetTransform()
    {
        return transform;
    }


}