using System;
using Assets.Scripts.GameManager;
using Assets.Scripts.DecisionMaking.GOB;
using UnityEngine;

namespace Assets.Scripts.DecisionMakingActions
{
    public class Shoot : WalkToTargetAndExecuteAction
    {
        public Shoot(AutonomousCharacter character, GameObject target) : base("Shoot",character,target)
        {
        }

        public override float GetGoalChange(Goal goal)
        {
            var change = base.GetGoalChange(goal);
            if (goal.Name == AutonomousCharacter.EAT_GOAL) change -= 2.0f;
            return change;
        }

        public override bool CanExecute()
        {
            if (!base.CanExecute()) return false;
            return (this.Character.GameManager.characterData.Arrows >= 1 && this.Character.GameManager.characterData.Hunger > 4.0f);
        }

        public override bool CanExecute(WorldModel worldModel)
        {
            if (!base.CanExecute(worldModel))
            {
                return false;
            }
            var arr = (int)worldModel.GetProperty(Properties.ARROWS);
            var hh = (float)worldModel.GetProperty(Properties.HUNGER);
            return arr >= 1 && hh > 4.0f;
        }

        public override void Execute()
        {
            base.Execute();
            this.Character.GameManager.Shoot(this.Target);
        }
        

        public override void ApplyActionEffects(WorldModel worldModel)
        {
            base.ApplyActionEffects(worldModel);

            var eatValue = worldModel.GetGoalValue(AutonomousCharacter.EAT_GOAL);
            worldModel.SetGoalValue(AutonomousCharacter.EAT_GOAL, eatValue - 2.0f);

            var hunger = (float)worldModel.GetProperty(Properties.HUNGER);
            worldModel.SetProperty(Properties.HUNGER, hunger - 2.0f);

            var arrows = (int)worldModel.GetProperty(Properties.ARROWS);
            worldModel.SetProperty(Properties.ARROWS, arrows - 1);

            //disables the target object so that it can't be reused again
            worldModel.SetProperty(this.Target.name, false);
        }
    }
}
