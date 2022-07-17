using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleDoDado : MonoBehaviour
{
    public Animator Menu;
    public int Selecionado;

    void Update()
    {
        Selecionado = Menu.GetInteger("Estado");

        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) && Selecionado < 3)
        {

            Selecionado ++;

            Menu.SetInteger("Estado", Selecionado);
            
        }
        
        if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) && Selecionado > 1)
        {
          
            Selecionado --;

            Menu.SetInteger("Estado", Selecionado);
        }
    }
}
