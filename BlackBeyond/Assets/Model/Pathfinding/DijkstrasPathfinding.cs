using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DijkstrasPathfinding 
{

    private List<PathfindingNode> allNodes;

    public DijkstrasPathfinding(Space startSpace, int maxCost)
    {
        allNodes = new List<PathfindingNode>();
        List<Space> shortestPath = new List<Space>
        {
            startSpace
        };
        PathfindingNode currentnode = new PathfindingNode(startSpace, true, shortestPath, 0);
        allNodes.Add(currentnode);

        bool done = false;
        while(!done)
        {
            foreach (Space adjacentSpace in currentnode.GetSpace().GetAdjacentSpaces())
            {
                if (adjacentSpace != null)
                {
                    PathfindingNode nextNode = adjacentSpace.GetNode();
                    if (nextNode != null)
                    {
                        // not null
                        if (!nextNode.BeenSeen())
                        {
                            // Next node hasn't been visited yet
                            nextNode.Update(currentnode.GetCost() + adjacentSpace.GetMovementCost(), shortestPath);
                        }
                    }
                    else
                    {
                        // Is null, need new node
                        int newNodeCost = currentnode.GetCost() + adjacentSpace.GetMovementCost();
                        if (newNodeCost <= maxCost)
                        {
                            List<Space> newNodePath = new List<Space>(shortestPath)
                        {
                            adjacentSpace
                        };
                            PathfindingNode newNode = new PathfindingNode(adjacentSpace, false, newNodePath, newNodeCost);
                            allNodes.Add(newNode);
                            adjacentSpace.SetNode(newNode);
                        }
                    }
                }
            }
            PathfindingNode lowestNode = null;
            foreach(PathfindingNode node in allNodes)
            {
                if(!node.BeenSeen())
                {
                    if(lowestNode == null)
                    {
                        lowestNode = node;
                    }
                    else
                    {
                        if(node.GetCost()<lowestNode.GetCost())
                        {
                            lowestNode = node;
                        }
                    }
                }
            }
            if (lowestNode == null)
            {
                done = true;
            }
            else
            {
                lowestNode.Seen();
                shortestPath.Add(lowestNode.GetSpace());
                currentnode = lowestNode;
                Debug.Log(lowestNode.GetCost() + " -> " + lowestNode.GetSpace().Row +":"+ lowestNode.GetSpace().Column);
            }
        }
    }

    public List<PathfindingNode> GetNodes()
    {
        return allNodes;
    }
}
