using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Transform CharacterStartPositionReference;

    private IResetable[] resetables;
    private IUpdatable[] updatables;

    public void Setup()
    {
        if (resetables == null)
            resetables = GetComponentsInChildren<IResetable>(true);

        updatables = GetComponentsInChildren<IUpdatable>(true);

        for (int i = 0; i < resetables.Length; i++)
        {
            resetables[i].SetupOnStartLevel();
        }

        CharacterController.Instance.SetStartPosition(CharacterStartPositionReference.position);
    }

    public void UpdateObjs()
    {
        if (updatables == null)
            return;

        for (int i = 0; i < updatables.Length; i++)
        {
            updatables[i].UpdateObj();
        }
    }

    public void ResetLevel()
    {
        CharacterController.Instance.ResetObject();

        for (int i = 0; i < resetables.Length; i++)
        {
            resetables[i].ResetObject();
        }
    }
}