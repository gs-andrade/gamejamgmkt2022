using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransicaoControl : MonoBehaviour
{
    public static TransicaoControl Instance;
    private Animator animator;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        if (Instance != this)
            Destroy(gameObject);

        animator = GetComponent<Animator>();
    }
    
    public void FadeInLevelChange()
    {
        animator.SetTrigger("FadeInLevelChange");
    }

    public void FadeInRestartLevel()
    {
        animator.SetTrigger("FadeInRestartLevel");
    }

    public void RestartLevel()
    {
        GameplayController.Instance.ResetLevel();
    }
        

    public void StartNextLevel()
    {
        GameplayController.Instance.StartNextLevel();
    }

    public void FadeIn()
    {
        animator.SetTrigger("SceneChange");
    }

    public void FadeOut()
    {
        animator.SetTrigger("SceneChange1");
    }


}
