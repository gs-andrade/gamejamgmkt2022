using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    public Animator Transition;
    private bool Colidio = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<CharacterInstance>();
        
        if (player != null && Colidio == false)
        {
            Colidio = true;
            Transition.SetTrigger("SceneChange");

            Invoke("TrocaCena", 1);
            
        }
    }

    public void TrocaCena()
    {
        Colidio = false;
        GameplayController.Instance.StartNextLevel();

    }
}
