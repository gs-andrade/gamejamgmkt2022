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

    [Header("Teste")]
    public bool ForceLevelStart;
    public int ForcedLevelStart;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        if (Instance != this)
            Destroy(this);

        if (AudioManeger.Instance == null)
        {
            LoadFromResources("AudioManeger");
        }

        if(TransicaoControl.Instance == null)
        {
            LoadFromResources("TransicaoControl");
        }

        if(CanvasEndGame.Instance == null)
        {
            LoadFromResources("CanvasEndGame");
        }

        levels = GetComponentsInChildren<Level>(true);
        levelIndex = -1;

        if (ForceLevelStart)
            levelIndex = ForcedLevelStart - 1;

        StartNextLevel();

    }

    private void Start()
    {
        AudioManeger.Instance.Play("Venus", 1, true);
    }

    private void LoadFromResources(string objectName)
    {
        var aux = Resources.Load<GameObject>(objectName);
        Instantiate(aux, transform);
    }

    public void StartNextLevel()
    {

        levelIndex++;

        if (levelIndex >= levels.Length)
        {
            if (CanvasEndGame.Instance != null)
                CanvasEndGame.Instance.ShowEndGamePanel();

            return;
        }

        if (levelCurrent != null)
            levelCurrent.gameObject.SetActive(false);

        levelCurrent = levels[levelIndex];
        levelCurrent.Setup();
        levelCurrent.gameObject.SetActive(true);

    }

    public void ResetLevel()
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
                        TransicaoControl.Instance.FadeInRestartLevel();
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