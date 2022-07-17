using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPatrol : EnemyGeneric
{

    public bool TurnAround = true;
    public float Speed = 5f;

    private DrawPoint[] points;
    private int moveIndex;
    private Vector2 nextLocation;
    private Transform cachedTf;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var character = collision.gameObject.GetComponent<CharacterInstance>();

        if (character != null)
        {
            if (DealDamage(character))
                SetNextDestination();
        }
    }


    public override void SetupOnStartLevel()
    {
        base.SetupOnStartLevel();

        cachedTf = transform;
        points = cachedTf.parent.GetComponentsInChildren<DrawPoint>(true);

        if (points.Length != 0)
            nextLocation = points[0].transform.position;
    }

    private void SetNextDestination()
    {
        moveIndex++;

        if (points == null)
            points = GetComponentsInChildren<DrawPoint>(true);

        if (moveIndex >= points.Length)
            moveIndex = 0;

        if (points.Length != 0)
            nextLocation = points[moveIndex].transform.position;
    }

    public override void ResetObject()
    {
        base.ResetObject();

        moveIndex = 0;
        //animator.SetBool("Alive", true);
    }

    public override void UpdateObj()
    {
        base.UpdateObj();

        if (!IsAlive())
            return;

        if (disableTime > 0)
        {
            return;
        }

        if (Vector2.Distance(cachedTf.position, nextLocation) < 0.05f)
        {
            SetNextDestination();
        }
        else
        {
            cachedTf.position = Vector2.MoveTowards(cachedTf.position, nextLocation, Speed * Time.deltaTime);

            if (TurnAround)
            {

                var direction = nextLocation.x - cachedTf.position.x;

                if (direction > 0)
                    cachedTf.localScale = new Vector3(-1, 1, 1);
                else
                    cachedTf.localScale = new Vector3(1, 1, 1);
            }
        }
    }
    
    public override bool DealDamage(CharacterInstance character)
    {
        if (!IsAlive())
            return false;

        //SoundController.instance.PlayAudioEffect("CrabAttack");
        return base.DealDamage(character);
    }

}