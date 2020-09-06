using Gameplay.Core.Actions;
using Gameplay.Core.Cards;
using UnityEngine;
using UnityEngine.Assertions;

namespace Gameplay.Core
{
    public class MatchReferee : MonoBehaviour
    {
        GameActionFactory GameActionFactory { get; set; }

        GameActionsQueue ActionsQueue { get; } = new GameActionsQueue();

        public void Setup(GameActionFactory gameActionFactory)
        {
            GameActionFactory = gameActionFactory;
            ActionsQueue.ScheduleAction(GameActionFactory.CreateDeployCardAction(CardType.Warrior));
        }

        void Start()
        {
            Assert.IsNotNull(GameActionFactory);
        }

        void Update()
        {
            ActionsQueue.Execute();
        }
    }
}
