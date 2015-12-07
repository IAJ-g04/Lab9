using Assets.Scripts.GameManager;
using Assets.Scripts.DecisionMaking.GOB;
using UnityEngine;
using Action = Assets.Scripts.DecisionMaking.GOB.Action;

namespace Assets.Scripts.DecisionMakingActions
{
    public abstract class WalkToTargetAndExecuteAction : CharacterAction
    {
        protected GameObject Target { get; set; }

        protected string Actuator { get { return "FollowPathActuator"; } }
        protected WalkToTargetAndExecuteAction(string actionName, AutonomousCharacter character, GameObject target) : base(actionName + "(" + target.name + ")", character)
        {
            this.Target = target;
        }

        public override float GetDuration()
        {
            //assume a velocity of 20.0f/s to get to the target
            return (this.Target.transform.position - this.Character.Character.KinematicData.position).magnitude / 20.0f;
        }

        public override float GetDuration(WorldModel worldModel)
        {
            //assume a velocity of 20.0f/s to get to the target
            var position = (Vector3)worldModel.GetProperty(Properties.POSITION);
            return (this.Target.transform.position - position).magnitude / 20.0f;
        }

        public override float GetGoalChange(Goal goal)
        {
            if (goal.Name == AutonomousCharacter.REST_GOAL)
            {
                var distance =
                    (this.Target.transform.position - this.Character.Character.KinematicData.position).magnitude;
                //+0.01 * distance because of the walk 
                return distance * 0.01f;
            }
            if (goal.Name == AutonomousCharacter.EAT_GOAL)
            {
                var distance =
                    (this.Target.transform.position - this.Character.Character.KinematicData.position).magnitude;
                //+0.01 * distance because of the walk 
                return distance * 0.1f;
            }
            else return 0;
        }


        public override bool CanExecute(WorldModel worldModel)
        {
            if (!base.CanExecute(worldModel)) return false;
            if (this.Target == null) return false;
            //Secret level 1
            var node = this.Character.navMesh.QuantizeToNode(this.Target.transform.position, 1.0f);

            float redInfluence = this.Character.RedInfluenceMap.GetInfluence(node);
            float greenInfluence = this.Character.GreenInfluenceMap.GetInfluence(node);

            float Security = redInfluence - greenInfluence;
            if (Security <= 0) return false;

            var targetEnabled = (bool)worldModel.GetProperty(this.Target.name);
            var distance =
                  (this.Target.transform.position - this.Character.Character.KinematicData.position).magnitude;
            if (((float)worldModel.GetProperty(Properties.ENERGY) - 0.5f <= (distance *0.01f)) &&
                ((float)worldModel.GetProperty(Properties.HUNGER) - 0.1f <= (distance * 0.1f)))
            {
                return false;
            }
            return targetEnabled;
        }

        public override void Execute()
        {
            this.Character.CharActuator.SwitchActuator(this.Actuator);
            this.Character.Targeter.UpdateGoal(this.Target.transform.position);
        }


        public override void ApplyActionEffects(WorldModel worldModel)
        {
            var duration = this.GetDuration(worldModel);
            var energyChange = duration * 0.01f;
            var hungerChange = duration * 0.1f;

            var restValue = worldModel.GetGoalValue(AutonomousCharacter.REST_GOAL);
            worldModel.SetGoalValue(AutonomousCharacter.REST_GOAL, restValue + energyChange);

            var energy = (float)worldModel.GetProperty(Properties.ENERGY);
            worldModel.SetProperty(Properties.ENERGY, energy - energyChange);

            var eatGoalValue = worldModel.GetGoalValue(AutonomousCharacter.EAT_GOAL);
            worldModel.SetGoalValue(AutonomousCharacter.EAT_GOAL, eatGoalValue + hungerChange);

            var hunger = (float)worldModel.GetProperty(Properties.HUNGER);
            worldModel.SetProperty(Properties.HUNGER, hunger + hungerChange);

            worldModel.SetProperty(Properties.POSITION, Target.transform.position);
        }

    }
}
