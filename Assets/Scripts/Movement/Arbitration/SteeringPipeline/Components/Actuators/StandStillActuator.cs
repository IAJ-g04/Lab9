using Assets.Scripts.Movement.DynamicMovement;
using Assets.Scripts.Pathfinding.Path;

namespace Assets.Scripts.Movement.Arbitration.SteeringPipeline.Components.Actuators
{
    public class StandStillActuator : ActuatorComponent
    {

        public override string Name
        {
            get { return "StandStillActuator"; }
        }

        private DynamicStop StopMovement { get; set; }
        public StandStillActuator(SteeringPipeline pipeline) : base(pipeline)
        {
            this.StopMovement = new DynamicStop(this.Pipeline.Character)
            {
                MaxAcceleration = 40.0f,
                TimeToStop = 1.5f
            };
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
            return this.StopMovement.GetMovement();
        }
    }
}
