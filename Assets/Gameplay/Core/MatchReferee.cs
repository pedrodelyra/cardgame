using System.Collections.Generic;
using Gameplay.Core.Actions;
using UnityEngine;

namespace Gameplay.Core
{
    public class MatchReferee : MonoBehaviour
    {
        [SerializeField] Arena arena;

        Queue<IGameAction> ActionsQueue { get; } = new Queue<IGameAction>();

        void Start()
        {
            ScheduleAction(new DeployCardAction(CardType.Mage, arena));
        }

        void Update()
        {
            while (ActionsQueue.Count > 0)
            {
                var action = ActionsQueue.Dequeue();
                action.Execute();
            }
        }

        void ScheduleAction(IGameAction gameAction)
        {
            ActionsQueue.Enqueue(gameAction);
        }
    }
}
