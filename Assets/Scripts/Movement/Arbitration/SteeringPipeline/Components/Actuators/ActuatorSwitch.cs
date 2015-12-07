using Assets.Scripts.Movement.DynamicMovement;
using Assets.Scripts.Pathfinding.Path;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Movement.Arbitration.SteeringPipeline.Components.Actuators
{
    public class ActuatorSwitch : ActuatorComponent
    {
        public override string Name
        {
            get { return "ActuatorSwitch"; }
        }

        public string ActiveActuator { get; set; }
        public Dictionary<string, ActuatorComponent> Actuators { get; private set; }
        public ActuatorSwitch(SteeringPipeline pipeline) : base(pipeline)
        {
            this.ActiveActuator = this.Name;
            this.Actuators = new Dictionary<string, ActuatorComponent>();
        }

        public void SwitchActuator(string actuator)
        {
            if ((this.ActiveActuator != actuator) && (Actuators.ContainsKey(actuator)))
            {
                this.ActiveActuator = actuator;
            }
        }

        public override Path GetPath(SteeringGoal goal)
        {
            return new LineSegmentPath(this.Pipeline.Character.position,goal.Position);
        }

        public override Path GetPath(GlobalPath path)
        {
            return path;
        }

        public override MovementOutput GetSteering(Path path)
        {
            if (Actuators.ContainsKey(ActiveActuator))
            {
                return Actuators[ActiveActuator].GetSteering(path);
            }
            return new MovementOutput();
        }
    }
}
