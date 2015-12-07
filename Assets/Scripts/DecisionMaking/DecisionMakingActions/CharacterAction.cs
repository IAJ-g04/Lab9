using Assets.Scripts.GameManager;
using Assets.Scripts.DecisionMaking.GOB;
using UnityEngine;
using Action = Assets.Scripts.DecisionMaking.GOB.Action;
using System;

namespace Assets.Scripts.DecisionMakingActions
{
    public abstract class CharacterAction : Action
    {
        protected AutonomousCharacter Character { get; set; }

        protected CharacterAction(string actionName, AutonomousCharacter character) : base(actionName)
        {
            this.Character = character;
        }

        public override float GetDuration()
        {

            throw new NotImplementedException();
        }

        public override float GetDuration(WorldModel worldModel)
        {

            throw new NotImplementedException();
        }

        public override float GetGoalChange(Goal goal)
        {

            throw new NotImplementedException();
        }

        public override bool CanExecute()
        {

            throw new NotImplementedException();
        }

        public override bool CanExecute(WorldModel worldModel)
        {


            throw new NotImplementedException();
        }

        public override void Execute()
        {

            throw new NotImplementedException();

        }


        public override void ApplyActionEffects(WorldModel worldModel)
        {

            throw new NotImplementedException();
        }

    }
}
