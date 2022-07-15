using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAnimation 
{
    private AnimationState animationState = AnimationState.NotStarted;
    public abstract void Play();

    public AnimationState GetAnimationState()
    {
        return animationState;
    }

    protected void SetAnimationState(AnimationState state)
    {
        animationState = state;
    }

    public bool IsFinished() 
    {
        return animationState == AnimationState.Finished;
    }

    public void ResetAnimationState()
    {
        animationState = AnimationState.NotStarted;
    }
    public abstract void Refresh();
}
