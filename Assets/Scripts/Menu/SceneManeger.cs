using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManeger : MonoBehaviour
{
    //public ControleDoDado Estado;
    public Animator Transition;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.KeypadEnter))
        {
            Transition.SetTrigger("SceneChange");
            Invoke("TrocaCena", 1);
        }
    }

    public void TrocaCena()
    {
            SceneManager.LoadScene(1);
        //chama a cena com o numero do estado
        //if(Estado.Selecionado == 2)
        //{
            
        //    SceneManager.LoadScene(1);
        //}

        //if (Estado.Selecionado == 3)
        //{
        //    Application.Quit(0);
        //    Debug.Log("Saiu");
        //}
    }
}
