using System.Collections.Generic;

namespace Gameplay.Core.Actions
{
    public class GameActionsQueue
    {
        Queue<IGameAction> ActionsQueue { get; } = new Queue<IGameAction>();

        public void Execute()
        {
            while (ActionsQueue.Count > 0)
            {
                var action = ActionsQueue.Dequeue();
                action.Execute();
            }
        }

        public void ScheduleAction(IGameAction gameAction)
        {
            if (gameAction.Validate())
            {
                ActionsQueue.Enqueue(gameAction);
            }
        }
    }
}
