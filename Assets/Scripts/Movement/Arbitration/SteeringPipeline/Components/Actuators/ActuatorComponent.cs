﻿using Assets.Scripts.Pathfinding.Path;

namespace Assets.Scripts.Movement.Arbitration.SteeringPipeline.Components.Actuators
{
    //An actuator turns a goal into a Path: taking the character's capabilities into account.
    public abstract class ActuatorComponent : SteeringPipelineComponent
    {
        public abstract string Name { get; }
        protected ActuatorComponent(SteeringPipeline pipeline) : base(pipeline)
        {
        }
        public abstract Path GetPath(SteeringGoal goal);

        public abstract Path GetPath(GlobalPath path);

        public abstract MovementOutput GetSteering(Path path);

    }
}
