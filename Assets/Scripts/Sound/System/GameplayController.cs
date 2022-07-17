using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public static GameplayController Instance;

    private GameState state;

    public Animator Transition;

    private Level[] levels;
    private Level levelCurrent;

    private int levelIndex;

    [Header("Teste")]
    public bool ForceLevelStart;
    public int ForcedLevelStart;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        if (Instance != this)
            Destroy(gameObject);

        levels = GetComponentsInChildren<Level>(true);
        levelIndex = -1;

        if (ForceLevelStart)
            levelIndex = ForcedLevelStart - 1;

        StartNextLevel();

    }

    public void StartNextLevel()
    {

        levelIndex++;

        if (levelIndex >= levels.Length)
        {
            Debug.Log("End Game");
            return;
        }
        
        if (levelCurrent != null)
            levelCurrent.gameObject.SetActive(false);

        levelCurrent = levels[levelIndex];
        levelCurrent.Setup();
        levelCurrent.gameObject.SetActive(true);

        if (levelIndex > 0)
        {
            Transition.SetTrigger("SceneChange1");
        }
    }

    private void ResetLevel()
    {
        CharacterController.Instance.ResetObject();
        levelCurrent.ResetLevel();
    }

    private void Update()
    {

        switch (state)
        {
            case GameState.InGame:
                {

                    CharacterController.Instance.UpdateCharacter();

                    if (levelCurrent != null)
                        levelCurrent.UpdateObjs();
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