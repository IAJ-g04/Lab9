using System;
using UnityEngine;

namespace Assets.Scripts.Movement.Arbitration.SteeringPipeline.Components.Targeters
{
    public class FixedTargeter : TargeterComponent
    {
        public SteeringGoal Target { get; set; }

        public FixedTargeter(SteeringPipeline pipeline) : base(pipeline)
        {
            this.Target = new SteeringGoal();
        }

        public override SteeringGoal GetGoal()
        {
            return this.Target;
        }

        public void UpdateGoal(SteeringGoal goal)
        {
            this.Target = goal;
        }

        public void UpdateGoal(Vector3 position)
        {
            this.Target.Position = position;
        }
    }
}
