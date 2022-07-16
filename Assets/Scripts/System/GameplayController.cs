using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public static GameplayController Instance;

    private GameState state;

    private Level[] levels;
    private Level levelCurrent;

    private int levelIndex;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        if (Instance != this)
            Destroy(gameObject);

        levels = GetComponentsInChildren<Level>(true);
        levelIndex = -1;
        StartNextLevel();

    }

    public void StartNextLevel()
    {
        levelIndex++;

        if(levelIndex >= levels.Length)
        {
            Debug.Log("End Game");
            return;
        }

        if (levelCurrent != null)
            levelCurrent.gameObject.SetActive(false);

        levelCurrent = levels[levelIndex];
        levelCurrent.Setup();
        levelCurrent.gameObject.SetActive(true);
    }

    private void ResetLevel()
    {
        CharacterController.Instance.ForceState(CharacterState.Normal);
        levelCurrent.ResetLevel();
    }

    private void Update()
    {

        switch (state)
        {
            case GameState.InGame:
                {

                    CharacterController.Instance.UpdateCharacter();

#if UNITY_EDITOR
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        ResetLevel();
                    }
#endif
                    break;
                }



        }
    }


}

public enum GameState
{
    InGame,
}