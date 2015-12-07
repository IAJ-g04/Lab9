using System;
using Assets.Scripts.DecisionMaking.GOB;
using UnityEngine;

namespace Assets.Scripts.DecisionMakingActions
{
    public class PlaceFlag : WalkToPositionAndExecuteAction
    {
        public PlaceFlag(AutonomousCharacter character) : base("PlaceFlag", character)
        {
        }

        public override float GetGoalChange(Goal goal)
        {
            var change = base.GetGoalChange(goal);
            if (goal.Name == AutonomousCharacter.CONQUER_GOAL) change -= 2.0f;
            return change;
        }

    
        public override bool CanExecute(WorldModel worldModel)
        {
            if (!base.CanExecute(worldModel)) return false;
            return (worldModel.GetGoalValue(AutonomousCharacter.CONQUER_GOAL) > 8.5f && worldModel.GetGoalValue(AutonomousCharacter.EAT_GOAL) < 8.5f);
        }

        public override void Execute()
        {

            base.Execute();
            this.Character.GameManager.PlaceFlag(this.Position);
        }


        public override void ApplyActionEffects(WorldModel worldModel)
        {
            base.ApplyActionEffects(worldModel);

            var conqValue = worldModel.GetGoalValue(AutonomousCharacter.CONQUER_GOAL);
            worldModel.SetGoalValue(AutonomousCharacter.CONQUER_GOAL, conqValue - 2.0f);


        }
    }
}
