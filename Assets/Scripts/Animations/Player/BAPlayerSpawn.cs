using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAPlayerSpawn : BaseAnimation
{
    private Transform playerTransform;
    private float speed = 6f;
    public void PreLoad(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
    }
    public override void Play()
    {
        playerTransform.localPosition = new Vector2(-10, 0);
        playerTransform.gameObject.SetActive(true);
        SetAnimationState(AnimationState.Playing);
    }

    public override void Refresh()
    {
        playerTransform.Translate(Vector2.right * speed * Time.deltaTime);

        var posX = playerTransform.localPosition.x;

        if(posX >= -6)
        {
            SetAnimationState(AnimationState.Finished);
        }
    }
}
