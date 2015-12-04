using UnityEngine;

namespace Assets.Scripts.TacticalAnalysis
{
    public interface IInfluenceFunction
    {
        float DetermineInfluence(IInfluenceUnit unit, Vector3 location);
    }
}
