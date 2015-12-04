using RAIN.Navigation.Graph;

namespace Assets.Scripts.Pathfinding.Heuristics
{
    public class ZeroHeuristic : IHeuristic
    {
        public float H(NavigationGraphNode node, NavigationGraphNode goalNode)
        {
            return 0;
        }
    }
}
