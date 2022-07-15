namespace DAS
{
    public abstract class GameState
    {
        public abstract GameStateKey GetKey { get; }
        public abstract void OnEnterState();
        public abstract  GameStateKey OnUpdateState();
        public abstract void OnExitState();

    }
}



