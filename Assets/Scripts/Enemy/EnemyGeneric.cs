using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyGeneric : MonoBehaviour, IUpdatable, IResetable, IDamagable
{
    [Header("Generic Enemy Attributes")]
    public int LifeMax = 6;
    public EnemyDeathType DeathType;
    public SpriteRenderer Renderer;
    public TextMesh LifeText;

    protected EnemyState enemyState;
    private Color baseColor;

    protected int lifeCurrent;

    protected float disableTime;
    private float animationSpeed;

    protected Animator animator;
    protected Vector2 startPosition;
    public BoxCollider2D PlataformCollider;

    public virtual void SetupOnStartLevel()
    {
        baseColor = Renderer.color;

        startPosition = transform.position;

        enemyState = EnemyState.Normal;

        animator = GetComponentInChildren<Animator>(true);

        animationSpeed = animator.speed;

        SetCurrentLife(LifeMax);
    }

    public virtual void ResetObject()
    {
        transform.position = startPosition;
        gameObject.SetActive(true);
        SetCurrentLife(LifeMax);
    }

    public virtual void UpdateObj()
    {
        if (enemyState == EnemyState.Freeze)
        {
            return;
        }
    }
    public virtual bool DealDamage(CharacterInstance character)
    {
        if (enemyState == EnemyState.Freeze)
            return false;

        if (disableTime <= 0 && !character.IsVunerable())
        {
            character.TakeDamage(transform.position);
            return true;
        }

        return false;
    }

    public virtual void TakeDamage(Vector2 damagerDirection)
    {
        if (enemyState == EnemyState.Freeze)
        {
            Renderer.color = baseColor;
            PlataformCollider.enabled = false;
            enemyState = EnemyState.Normal;
            SetCurrentLife(LifeMax);
            animator.speed = animationSpeed;
        }
        else
        {
            SetCurrentLife(lifeCurrent - 1);
        }
    }

    public virtual void SetLifeText(string newText)
    {
        if (LifeText != null)
            LifeText.text = newText;
    }

    protected void SetCurrentLife(int life)
    {
        lifeCurrent = life;

        if (lifeCurrent <= 0)
        {
            switch (DeathType)
            {
                case EnemyDeathType.Normal:
                    {
                        //play animation
                        gameObject.SetActive(false);
                        break;
                    }

                case EnemyDeathType.Freeze:
                    {
                        Renderer.color = Color.cyan;
                        enemyState = EnemyState.Freeze;
                        animator.speed = 0;
                        PlataformCollider.enabled = true;
                        break;
                    }

                case EnemyDeathType.ResetNumber:
                    {
                        if (LifeMax <= 0)
                        {
                            Debug.LogError("Vida max n pode ser 0");
                            LifeMax = 1;
                        }

                        SetCurrentLife(LifeMax);
                        break;
                    }
            }
        }


        SetLifeText(lifeCurrent.ToString());
    }

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

    public int GetCurrentLife()
    {
        return lifeCurrent;
    }


}

public enum EnemyState
{
    Normal,
    Freeze,
}
public enum EnemyDeathType
{
    Normal,
    Freeze,
    ResetNumber,
}