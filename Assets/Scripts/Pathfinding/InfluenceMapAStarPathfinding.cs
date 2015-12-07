using System.Collections.Generic;
using Assets.Scripts.Pathfinding.DataStructures;
using Assets.Scripts.Pathfinding.Heuristics;
using Assets.Scripts.Pathfinding.Path;
using RAIN.Navigation.Graph;
using RAIN.Navigation.NavMesh;
using UnityEngine;
using Assets.Scripts.TacticalAnalysis;
using Assets.Scripts.TacticalAnalysis.DataStructures;

namespace Assets.Scripts.Pathfinding
{
    public class InfluenceMapAStarPathfinding : NodeArrayAStarPathFinding
    {
        private InfluenceMap RedMap { get; set; }
        private InfluenceMap GreenMap { get; set; }

        public InfluenceMapAStarPathfinding(NavMeshPathGraph graph, IHeuristic heuristic, InfluenceMap redMap, InfluenceMap greenMap) : base(graph,heuristic)
        {
            this.RedMap = redMap;
            this.GreenMap = greenMap;
        }

        protected new void ProcessChildNode(NodeRecord bestNode, NavigationGraphEdge connectionEdge)
        {
            float f;
            float g;
            float h;

            var childNode = connectionEdge.ToNode;
            var childNodeRecord = this.NodeRecordArray.GetNodeRecord(childNode);

            if (childNodeRecord == null)
            {
                //this piece of code is used just because of the special start nodes and goal nodes added to the RAIN Navigation graph when a new search is performed.
                //Since these special goals were not in the original navigation graph, they will not be stored in the NodeRecordArray and we will have to add them
                //to a special structure
                //it's ok if you don't understand this, this is a hack and not part of the NodeArrayA* algorithm
                childNodeRecord = new NodeRecord
                {
                    node = childNode,
                    parent = bestNode,
                    status = NodeStatus.Unvisited
                };
                this.NodeRecordArray.AddSpecialCaseNode(childNodeRecord);
            }


            // implement the rest of your code here

            if (childNodeRecord.status == NodeStatus.Closed) return;

            float influenceCost = CalculateInfluenceCost(bestNode.node, childNode);
            if (influenceCost < 0.0f)
                return;

            g = bestNode.gValue + connectionEdge.Cost - influenceCost;
            h = this.Heuristic.H(childNode, this.GoalNode);
            f = F(g,h);

            if (childNodeRecord.status == NodeStatus.Open)
            {
                if (f <= childNodeRecord.fValue)
                {
                    childNodeRecord.gValue = g;
                    childNodeRecord.hValue = h;
                    childNodeRecord.fValue = f;
                    childNodeRecord.parent = bestNode;
                    this.NodeRecordArray.Replace(childNodeRecord,childNodeRecord);
                }
            }
            else
            {
                childNodeRecord.gValue = g;
                childNodeRecord.hValue = h;
                childNodeRecord.fValue = f;
                childNodeRecord.status = NodeStatus.Open;
				childNodeRecord.parent = bestNode;
                this.NodeRecordArray.AddToOpen(childNodeRecord);
            }
        }

        protected float CalculateInfluenceCost(NavigationGraphNode node, NavigationGraphNode child)
        {
            float nodeRedInfluence = this.RedMap.GetInfluence(this.Quantize(node.Position));
            float nodeGreenInfluence = this.GreenMap.GetInfluence(this.Quantize(node.Position));

            float childRedInfluence = this.RedMap.GetInfluence(this.Quantize(child.Position));
            float childGreenInfluence = this.GreenMap.GetInfluence(this.Quantize(child.Position));

            float securityNodetoChild = nodeRedInfluence - nodeGreenInfluence;
            float securityChildtoNode = childRedInfluence - childGreenInfluence;

            return (securityChildtoNode + securityNodetoChild) / 2;
        }
    }
}
