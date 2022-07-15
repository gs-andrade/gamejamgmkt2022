using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DAS 
{
    public class GameStateFactory
    {
        public Dictionary<GameStateKey, GameState> gameStatesDic;

        public GameStateFactory()
        {
            var gameStates = Assembly.GetAssembly(typeof(GameState)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(GameState)));

            gameStatesDic = new Dictionary<GameStateKey, GameState>();

            foreach(var state in gameStates)
            {
                var newState = Activator.CreateInstance(state) as GameState;
                gameStatesDic.Add(newState.GetKey, newState);
            }
        }

        public GameState GetState(GameStateKey state)
        {
            if (gameStatesDic.ContainsKey(state))
            {
                return gameStatesDic[state];
            }

            return null;
        }

        public int GetStatesLength()
        {
            return gameStatesDic.Count;
        }
    }
}
