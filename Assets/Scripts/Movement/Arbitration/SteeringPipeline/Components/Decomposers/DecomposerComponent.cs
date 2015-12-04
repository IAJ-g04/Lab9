using Assets.Scripts.Pathfinding.Path;

namespace Assets.Scripts.Movement.Arbitration.SteeringPipeline.Components.Decomposers
{
    //A decomposer takes a goal and decomposes it into a a sub-goal
    public abstract class DecomposerComponent : SteeringPipelineComponent
    {
        public DecomposerComponent(SteeringPipeline pipeline) : base(pipeline)
        {
        }
        public abstract SteeringGoal DecomposeGoal(SteeringGoal goal);

        public abstract GlobalPath DecomposeGoalIntoPath(SteeringGoal goal);
    }
}
