using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DAS
{
    public class GameStateNormalBattle : GameState
    {
        public override GameStateKey GetKey => GameStateKey.NormalBattle;

        private PlayerController playerController;

        public override void OnEnterState()
        {
            if(playerController == null)
                playerController = GameManager.Instance.RequestController(ControllerKey.PlayerController) as PlayerController;

            playerController.Setup();

            //Executa animação de entrada
            //spawna player
            //spawna inimigo
        }

        public override void OnExitState()
        {
            //Vitorio ou GameOver?
        }

        public override GameStateKey OnUpdateState()
        {
            //controlar o player
            //AI do inimigo

            playerController.Refresh();

            return GameStateKey.NormalBattle;
        }
    }
}
