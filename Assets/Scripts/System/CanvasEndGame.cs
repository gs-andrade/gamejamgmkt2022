using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasEndGame : MonoBehaviour
{
    public static CanvasEndGame Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        if (Instance != this)
            Destroy(gameObject);
    }

    public void ShowEndGamePanel()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    
}
