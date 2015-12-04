using RAIN.Navigation.Graph;

namespace Assets.Scripts.Pathfinding.Heuristics
{
    public class EuclideanDistanceHeuristic : IHeuristic
    {
        public float H(NavigationGraphNode node, NavigationGraphNode goalNode)
        {
            return (goalNode.LocalPosition - node.LocalPosition).magnitude;
        }
    }
}
