using System;
using Assets.Scripts.GameManager;
using Assets.Scripts.DecisionMaking.GOB;
using Action = Assets.Scripts.DecisionMaking.GOB.Action;
using UnityEngine;

namespace Assets.Scripts.DecisionMakingActions
{
    public class Rest : StandStillAction
    {
        public Rest(AutonomousCharacter character) : base("Rest", character)
        {
            this.Duration = 0.5f;
        }

        public override float GetGoalChange(Goal goal)
        {
            var change = base.GetGoalChange(goal);
            if (goal.Name == AutonomousCharacter.REST_GOAL) change -= 0.1f;
            if (goal.Name == AutonomousCharacter.EAT_GOAL) change += 0.1f;
            return change;
        }

        

        public override bool CanExecute(WorldModel worldModel)
        {
//if (!base.CanExecute(worldModel)) return false;
            var energy = (float)worldModel.GetProperty(Properties.ENERGY);
            var hn = (float)worldModel.GetProperty(Properties.HUNGER);

            return (energy < 2.0f && hn < 9.5);
        }

        public override void ApplyActionEffects(WorldModel worldModel)
        {
            base.ApplyActionEffects(worldModel);

            var restValue = worldModel.GetGoalValue(AutonomousCharacter.REST_GOAL);
            worldModel.SetGoalValue(AutonomousCharacter.REST_GOAL, restValue - 0.1f);

            var energy = (float)worldModel.GetProperty(Properties.ENERGY);
            worldModel.SetProperty(Properties.ENERGY, energy + 0.1f);

            var eatValue = worldModel.GetGoalValue(AutonomousCharacter.EAT_GOAL);
            worldModel.SetGoalValue(AutonomousCharacter.EAT_GOAL, restValue + 0.1f);

            var hunger = (float)worldModel.GetProperty(Properties.HUNGER);
            worldModel.SetProperty(Properties.ENERGY, hunger + 0.1f);
        }

        public override void Execute()
        {
            base.Execute();
            this.Character.GameManager.Rest();
        }
    }
}
