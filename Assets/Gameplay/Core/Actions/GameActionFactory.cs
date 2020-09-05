using UnityEngine;

namespace Gameplay.Core.Actions
{
    public class GameActionFactory : MonoBehaviour
    {
        Arena Arena { get; }

        public GameActionFactory(Arena arena)
        {
            Arena = arena;
        }

        public DeployGameObjectAction CreateDeployCardAction()
        {
            
        }

        public IGameAction CreateAction<T>() where T : IGameAction, new()
        {
            var gameAction = new T();
            Setup(gameAction);
            return gameAction;
        }

        void Setup(IGameAction gameAction)
        {
            if (gameAction is IHasArena hasArena)
            {
                hasArena.Arena = Arena;
            }
        }
    }
}
