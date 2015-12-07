using Assets.Scripts.GameManager;
using Assets.Scripts.DecisionMaking.GOB;
using UnityEngine;
using Action = Assets.Scripts.DecisionMaking.GOB.Action;
using Assets.Scripts.Movement.Arbitration.SteeringPipeline;

namespace Assets.Scripts.DecisionMakingActions
{
    public abstract class StandStillAction : CharacterAction
    {
        
        protected string Actuator { get { return "StandStillActuator"; } }
        protected StandStillAction(string actionName, AutonomousCharacter character) : base(actionName, character)
        {
        }

        public override void Execute()
        {
            this.Character.CharActuator.SwitchActuator(this.Actuator);
            this.Character.Targeter.UpdateGoal(this.Character.transform.position);
        }
        

    }
}
