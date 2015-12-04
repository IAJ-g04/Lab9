using RAIN.Navigation.Graph;

namespace Assets.Scripts.Pathfinding.Heuristics
{
    public interface IHeuristic
    {
        float H(NavigationGraphNode node, NavigationGraphNode goalNode);
    }
}
