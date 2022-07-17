using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtons : MonoBehaviour
{
    public Animator Transition;

    public void Retry()
    {
        Transition.SetTrigger("SceneChange");
        SceneManager.LoadScene(1);
    }

    public void Menu()
    {
        Transition.SetTrigger("SceneChange");
        SceneManager.LoadScene(0);
    }
}
