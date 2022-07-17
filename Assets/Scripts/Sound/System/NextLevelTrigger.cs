using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour, IResetable
{
    public Animator Transition;

    private bool triggered;
    public void ResetObject()
    {
        triggered = false;
    }

    public void SetupOnStartLevel()
    {
        triggered = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<CharacterInstance>();

        if (player != null && !triggered)
        {
            triggered = true;
            TransicaoControl.Instance.FadeInLevelChange();
        }
    }
}
