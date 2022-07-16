using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChicoteAnimationTrigger : MonoBehaviour
{
    public void AttackStart()
    {
        CharacterController.Instance.AttackStart();
    }

    public void AttackFinish()
    {
        CharacterController.Instance.AttackFinish();
    }
}
