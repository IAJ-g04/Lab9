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

      

        public override bool CanExecute(WorldModel worldModel)
        {
            var en = (float)worldModel.GetProperty(Properties.ENERGY);
            return en > 0.3;        
        }
    }

    
}
