using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimationUtil
{
    private static Queue<BaseAnimation> listInOrderAnimations = new Queue<BaseAnimation>();

    private static List<BaseAnimation> listAnytimeAnimations = new List<BaseAnimation>();
    private static List<BaseAnimation> listToRemoveAnytimeAnimations = new List<BaseAnimation>();

    private static BaseAnimation currentInOrderAnimation;
    public static void Setup()
    {
        listInOrderAnimations.Clear();
        listAnytimeAnimations.Clear();
        listToRemoveAnytimeAnimations.Clear();
    }

    public static void AddInOrderAnimation(BaseAnimation animation)
    {
        if (CheckIfAnimationExists(animation))
        {
            Debug.LogError("Animation alredy exists");
            return;
        }

        listInOrderAnimations.Enqueue(animation);


        bool CheckIfAnimationExists(BaseAnimation animation)
        {
            for (int i = 0; i < listInOrderAnimations.Count; i++)
            {
                if (animation == listAnytimeAnimations[i])
                    return true;
            }

            return false;
        }

    }

    public static void AddAnytimeAnimation(BaseAnimation animation)
    {
        if (CheckIfAnimationExists(animation))
        {
            Debug.LogError("Animation alredy exists");
            return;
        }

        listAnytimeAnimations.Add(animation);


        bool CheckIfAnimationExists(BaseAnimation animation)
        {
            for (int i = 0; i < listAnytimeAnimations.Count; i++)
            {
                if (animation == listAnytimeAnimations[i])
                    return true;
            }

            return false;
        }



    }

    public static void Refresh()
    {
        RefreshInOrderAnimation();
        RefreshAnytimeAnimation();
    }

    private static void RefreshInOrderAnimation()
    {
        if (currentInOrderAnimation != null && !currentInOrderAnimation.IsFinished())
        {
            currentInOrderAnimation.Refresh();
            return;
        }

        if (listInOrderAnimations.Count == 0)
            return;

        currentInOrderAnimation = listInOrderAnimations.Dequeue();
        currentInOrderAnimation.Play();
    }

    private static void RefreshAnytimeAnimation()
    {
        for (int i = 0; i < listAnytimeAnimations.Count; i++)
        {
            var animation = listAnytimeAnimations[i];

            switch (animation.GetAnimationState())
            {
                case AnimationState.NotStarted:
                    animation.Play();
                    break;

                case AnimationState.Playing:
                    animation.Refresh();
                    break;

                case AnimationState.Finished:
                    listToRemoveAnytimeAnimations.Add(animation);
                    animation.ResetAnimationState();
                    break;
            }
        }

        for (int i = 0; i < listToRemoveAnytimeAnimations.Count; i++)
        {
            var animation = listToRemoveAnytimeAnimations[i];
            listAnytimeAnimations.Remove(animation);
        }

        listToRemoveAnytimeAnimations.Clear();
    }

  

  

}
