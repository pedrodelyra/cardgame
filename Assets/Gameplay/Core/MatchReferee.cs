using Gameplay.Core.Actions;
using Gameplay.Core.Cards;
using UnityEngine;

namespace Gameplay.Core
{
    public class MatchReferee : MonoBehaviour
    {
        GameActionFactory GameActionFactory { get; set; }

        GameActionsQueue ActionsQueue { get; } = new GameActionsQueue();

        public void Setup(GameActionFactory gameActionFactory)
        {
            GameActionFactory = gameActionFactory;
            ActionsQueue.ScheduleAction(GameActionFactory.CreateDeployCardAction(CardType.Warrior, Team.Home, 0));
            ActionsQueue.ScheduleAction(GameActionFactory.CreateDeployCardAction(CardType.Warrior, Team.Home, 0));
            ActionsQueue.ScheduleAction(GameActionFactory.CreateDeployCardAction(CardType.Warrior, Team.Home, 0));
            ActionsQueue.ScheduleAction(GameActionFactory.CreateDeployCardAction(CardType.Warrior, Team.Visitor, 0));
        }

        void Update() => ActionsQueue.Execute();
    }
}
