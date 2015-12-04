using RAIN.Navigation.Graph;

namespace Assets.Scripts.TacticalAnalysis
{
    public interface IInfluenceUnit
    {
        NavigationGraphNode Location { get; }
        float DirectInfluence { get; }
    }
}
