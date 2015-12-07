using Assets.Scripts.GameManager;
using Assets.Scripts.DecisionMaking.GOB;
using UnityEngine;

namespace Assets.Scripts.DecisionMakingActions
{
    public class Sleep : WalkToTargetAndExecuteAction
    {
        public Sleep(AutonomousCharacter character, GameObject target) : base("Sleep",character,target)
        {
        }

        public override float GetGoalChange(Goal goal)
        {
            var change = base.GetGoalChange(goal);
            if (goal.Name == AutonomousCharacter.REST_GOAL) change -= 1.0f;
            return change;
        }

       

        public override bool CanExecute(WorldModel worldModel)
        {
            if (!base.CanExecute(worldModel)) return false;

            return (worldModel.GetGoalValue(AutonomousCharacter.REST_GOAL) > 2.5f && worldModel.GetGoalValue(AutonomousCharacter.EAT_GOAL) < 8.5);
        }

        public override void ApplyActionEffects(WorldModel worldModel)
        {
            base.ApplyActionEffects(worldModel);

            var restValue = worldModel.GetGoalValue(AutonomousCharacter.REST_GOAL);
            worldModel.SetGoalValue(AutonomousCharacter.REST_GOAL,restValue-1.0f);

            var energy = (float)worldModel.GetProperty(Properties.ENERGY);
            worldModel.SetProperty(Properties.ENERGY, energy + 1.0f);
        }

        public override void Execute()
        {
            base.Execute();
            this.Character.GameManager.Sleep(this.Target);
        }
    }
}
