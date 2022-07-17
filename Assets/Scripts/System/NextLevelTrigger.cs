using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    private bool triggered = false;

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
