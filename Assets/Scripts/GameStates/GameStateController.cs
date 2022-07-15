using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DAS
{
    public class GameStateController : MonoBehaviour, IController
    {
        private GameStateFactory gameStateFactory = new GameStateFactory();
        private GameState gameStateActive;

        public bool ToogleDebug = false;
        public void Setup()
        {
            ChangeState(GameStateKey.NormalBattle);
        }

        public void PreLoad()
        {

        }

        private void Update()
        {
            UpdateStateActive();
            AnimationUtil.Refresh();
        }

        private void UpdateStateActive()
        {
            if (gameStateActive == null)
                return;

            ChangeState(gameStateActive.OnUpdateState());
        }
        private void ChangeState(GameStateKey key)
        {
            if (gameStateActive != null)
            {
                if (gameStateActive.GetKey == key)
                    return;

                gameStateActive.OnExitState();
            }
                

#if UNITY_EDITOR
            if (ToogleDebug)
            {
                Debug.Log($"Exit state {gameStateActive != null : gameStateActive.GetKey}");
                Debug.Log($"Entenring state {key}");
            }
#endif

            gameStateActive = gameStateFactory.GetState(key);
            gameStateActive.OnEnterState();
        }

        public ControllerKey GetKey()
        {
            return ControllerKey.GameStateController;
        }
    }

}
