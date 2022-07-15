using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DAS
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private bool initialized = false;

        private GameStateController gameStateController;

        private Dictionary<ControllerKey, IController> controller = new Dictionary<ControllerKey, IController>();

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;

            DontDestroyOnLoad(gameObject);

            if (!initialized)
                Initialize();
        }

        private void Initialize()
        {
            var allControllers = Resources.LoadAll("Controllers");

            var cachedTransform = transform;

            for (int i = 0; i < allControllers.Length; i++)
            {
                var aux = Instantiate((GameObject)allControllers[i], cachedTransform).GetComponent<IController>();
                controller.Add(aux.GetKey(), aux);
                aux.PreLoad();
            }

            gameStateController = GetController(ControllerKey.GameStateController) as GameStateController;
            gameStateController.Setup();
        }

        public IController RequestController(ControllerKey key)
        {
            return GetController(key);
        }

        private IController GetController(ControllerKey key)
        {
            return controller[key];
        }



        private List<IResetable> resetables = new List<IResetable>();
        public void RegisterToReset(IResetable resetable)
        {
            resetables.Add(resetable);
        }

        private void ResetWorld()
        {
            for (int i = 0; i < resetables.Count; i++)
            {
                resetables[i].ResetObject();
            }
        }

    }

}