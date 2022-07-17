using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManeger : MonoBehaviour
{
    public ControleDoDado Estado;
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            TrocaCena();
        }
    }

    public void TrocaCena()
    {
        //chama a cena com o numero do estado

        if (Estado.Selecionado == 3)
        {
            Application.Quit(0);
        }
    }
}
