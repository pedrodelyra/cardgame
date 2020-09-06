using Gameplay.Core.Actions;
using UnityEngine;

namespace Gameplay.Core
{
    public class GameRoom : MonoBehaviour
    {
        [SerializeField] Arena arena;
        [SerializeField] MatchReferee matchReferee;

        void Start()
        {
            GameActionFactory gameActionFactory = new GameActionFactory(arena);
            matchReferee.Setup(gameActionFactory);
        }
    }
}
