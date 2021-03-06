﻿namespace Assets.Scripts.Movement.Arbitration.SteeringPipeline.Components
{
    public abstract class SteeringPipelineComponent
    {
        public SteeringPipeline Pipeline { get; private set; }

        public SteeringPipelineComponent(SteeringPipeline pipeline)
        {
            this.Pipeline = pipeline;
        }
    }
}
