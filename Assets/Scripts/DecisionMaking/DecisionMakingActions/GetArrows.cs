using System;
using Assets.Scripts.GameManager;
using Assets.Scripts.DecisionMaking.GOB;
using UnityEngine;

namespace Assets.Scripts.DecisionMakingActions
{
    public class GetArrows : WalkToTargetAndExecuteAction
    {
        public GetArrows(AutonomousCharacter character, GameObject target) : base("GetArrows",character,target)
        {
        }

        public override bool CanExecute()
        {
            if (!base.CanExecute()) return false;
            var hg = this.Character.EatGoal.InsistenceValue;
            var sg = 1 - this.Character.SurviveGoal.InsistenceValue;
            if (sg > 2)
            {
                return hg < 8.0f;
            }
            else
            {
                return true;
            }
        }

        public override bool CanExecute(WorldModel worldModel)
        {
            if (!base.CanExecute(worldModel)) return false;
            var hg = (float)worldModel.GetProperty(Properties.HUNGER);
            var sg = (int)worldModel.GetProperty(Properties.HP);
            if (sg > 2)
            {
                return hg < 8.0f;
            }
            else
            {
                return true;
            }
        }

        public override void Execute()
        {
            base.Execute();
            this.Character.GameManager.GetArrows(this.Target);
        }


        public override void ApplyActionEffects(WorldModel worldModel)
        {
            base.ApplyActionEffects(worldModel);
            
            var arrows = (int)worldModel.GetProperty(Properties.ARROWS);
            worldModel.SetProperty(Properties.ARROWS, arrows + 10);
            
            //disables the target object so that it can't be reused again
            worldModel.SetProperty(this.Target.name, false);
        }


    }
}
