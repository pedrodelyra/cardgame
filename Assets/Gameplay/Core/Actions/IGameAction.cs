namespace Gameplay.Core.Actions
{
    public interface IGameAction
    {
        bool Validate();

        void Execute();
    }

    public abstract class GameAction<T> : IGameAction
    {
        protected T Data { get; private set; }

        public void Setup(T data) => Data = data;

        public abstract bool Validate();

        public abstract void Execute();
    }
}
