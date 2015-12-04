using System;
using UnityEngine;

namespace Assets.Scripts.TacticalAnalysis
{
    public class LinearInfluenceFunction : IInfluenceFunction
    {
        public float DetermineInfluence(IInfluenceUnit unit, Vector3 location)
        {
            return unit.DirectInfluence / (1 + (location - unit.Location.Position).magnitude);
        }
    }
}
