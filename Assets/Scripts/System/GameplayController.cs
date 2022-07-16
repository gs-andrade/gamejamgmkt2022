using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public static GameplayController Instance;
    public float TotalTimeBeforeReset;

    private GameState state;

    private float currentTimeBeforeReset;

    private List<int> keys = new List<int>();

    private List<IResetable> resetables = new List<IResetable>();


    private float tempoDecorrido;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

       if (Instance != this)
        Destroy(gameObject);

    }

    private void Start()
    {
        ResetWorld();
    }

    public bool HasKey(int key)
    {
        for (int i = 0; i < keys.Count; i++)
        {
            if (keys[i] == key)
                return true;
        }

        return false;
    }

    public void AddKey(int key)
    {
        keys.Add(key);
    }

    public void RegisterToReset(IResetable resetable)
    {
        resetables.Add(resetable);
    }

    private void ResetWorld()
    {
       // SoundController.instance.PlayAudioEffect("gamejaaj", SoundAction.Reset);
        CharacterController.Instance.ForceState(CharacterState.Normal);

        for (int i = 0; i < resetables.Count; i++)
        {
            resetables[i].ResetObject();
        }
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
                        ResetWorld();
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