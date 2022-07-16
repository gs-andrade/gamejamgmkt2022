using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterInstance : MonoBehaviour, IDamagable
{
    [Header("Ground Collision Checks")]
    public Vector2 RaycastFootOffset;
    public float RayCastFootLenght;
    public LayerMask GroundLayer;
    public bool moveChar = false;

    [Header("Status")]
    public float InvunerabilityTime;
    public Vector2 KnocbackForce;
    public float DisableTime;


    private Transform cachedTf;
    private BoxCollider2D BoxCollider;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer renderer;

    private float disableTimer;
    private float invunerabilityTimer;
    public void Setup()
    {
        cachedTf = transform;
        BoxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>(true);
        renderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void SnapToGround()
    {
        var centerCheck = CollisionUtils.CustomRaycast(cachedTf.position, Vector2.zero, Vector2.down, RayCastFootLenght, GroundLayer, true);
        if (centerCheck && centerCheck.collider != null)
        {
            var yPos = centerCheck.collider.gameObject.transform.position.y + RayCastFootLenght;
            transform.position = new Vector2(transform.position.x, yPos);
            SetXVelocity(0);
        }
    }
    public bool CheckIfIsOnGround()
    {
        BoxCollider.enabled = false;

        var leftCheck = CollisionUtils.CustomRaycast(cachedTf.position, -RaycastFootOffset, Vector2.down, RayCastFootLenght, GroundLayer, true);
        var rightCheck = CollisionUtils.CustomRaycast(cachedTf.position, RaycastFootOffset, Vector2.down, RayCastFootLenght, GroundLayer, true);
        var centerCheck = CollisionUtils.CustomRaycast(cachedTf.position, Vector2.zero, Vector2.down, RayCastFootLenght, GroundLayer, true);

        BoxCollider.enabled = true;

        if (leftCheck || rightCheck || centerCheck)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public void SetGravity(float ammount)
    {
        rb.gravityScale = ammount;
    }

    public void SetMovement(Vector2 movement, bool changeDirection = true)
    {
        rb.velocity = movement;

        if (movement != Vector2.zero)
        {
            if (changeDirection)
            {
                if (movement.x > 0)
                    cachedTf.localScale = new Vector3(1, 1, 1);
                else if (movement.x < 0)
                    cachedTf.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    public float GetDirection()
    {
        return cachedTf.localScale.x;
    }

    public void Jump(float force)
    {
        rb.velocity = new Vector2(rb.velocity.x, force);
    }

    public void SetXVelocity(float xMove, bool changeDirection = true)
    {
        rb.velocity = new Vector2(xMove, rb.velocity.y);

        if (xMove != 0)
        {
            if (changeDirection)
            {
                if (xMove > 0)
                    transform.localScale = new Vector3(1, 1, 1);
                else
                    transform.localScale = new Vector3(-1, 1, 1);
            }
        }

    }
    public void SetYVelocity(float yMOve)
    {
        rb.velocity = new Vector2(rb.velocity.x, yMOve);
    }

    public void SetAnimationTrigger(string name)
    {
        animator.SetTrigger(name);
    }

    public void SetAnimationBool(string name, bool state)
    {
        animator.SetBool(name, state);
    }


    public bool IsVunerable()
    {
        return invunerabilityTimer > 0;
    }

    public bool IsDisabled()
    {
        return disableTimer > 0;
    }

    public void TakeDamage(Vector2 damagerPosition)
    {
        if (invunerabilityTimer > 0)
            return;

        var dirX = cachedTf.position.x - damagerPosition.x >= 0 ? 1 : -1;

        invunerabilityTimer = InvunerabilityTime;

        SetMovement(new Vector2(KnocbackForce.x * dirX, KnocbackForce.y), false);
        disableTimer = DisableTime;

        CharacterController.Instance.TakeDamage();
        SetAnimationTrigger("TakeDamage");
        SetAnimationBool("IsWalking", false);


        SoundController.instance.PlayAudioEffect("damaged1");
    }


    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(cachedTf.position);
            Debug.Log("Pressed K");
        }

        if (disableTimer > 0)
            disableTimer -= Time.deltaTime;

        if (invunerabilityTimer > 0)
        {
            invunerabilityTimer -= Time.deltaTime;
            renderer.enabled = !renderer.enabled;
        }
        else
        {
            renderer.enabled = true;
        }
    }

    public bool IsAlive()
    {
        throw new System.NotImplementedException();
    }

    public Vector2 GetPosition()
    {
        return cachedTf.position;
    }

    public Transform GetTransform()
    {
        return cachedTf;
    }
}