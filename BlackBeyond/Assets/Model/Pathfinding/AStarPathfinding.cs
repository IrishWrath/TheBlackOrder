using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class AStarPathfinding
{
    public static List<SpaceModel> GetPathToDestination(SpaceModel startSpace, SpaceModel destSpace)
    {
        List<PathfindingNode> allNodes = new List<PathfindingNode>();

        PathfindingNode currentnode = new PathfindingNode(startSpace, null, 0, destSpace, true);
        allNodes.Add(currentnode);
        bool done = false;
        while (!done)
        {
            foreach (SpaceModel adjacentSpace in currentnode.GetSpace().GetAdjacentSpaces())
            {
                if (adjacentSpace != null)
                {
                    if (adjacentSpace.GetNode() == null)
                    {
                        // Is null, need new node
                        int newNodeCost = currentnode.GetCost() + adjacentSpace.GetMovementCost();

                        PathfindingNode newNode = new PathfindingNode(adjacentSpace, currentnode, newNodeCost, destSpace, false);
                        allNodes.Add(newNode);
                        adjacentSpace.SetNode(newNode);
                    }
                    else
                    {
                        // not null, there is a node here
                        PathfindingNode nextNode = adjacentSpace.GetNode();
                        if (!nextNode.BeenSeen())
                        {
                            // Next node hasn't been visited yet
                            nextNode.Update(currentnode.GetCost() + adjacentSpace.GetMovementCost(), currentnode);
                        }
                    }
                }
            }
            currentnode.Seen();
            if (!currentnode.GetSpace().Equals(destSpace))
            {
                PathfindingNode lowestNode = null;
                foreach (PathfindingNode node in allNodes)
                {
                    if (!node.BeenSeen())
                    {
                        if (lowestNode == null)
                        {
                            lowestNode = node;
                        }
                        else
                        {
                            if (node.GetASCost() < lowestNode.GetASCost())
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
                    currentnode = lowestNode;
                }
            }
            else
            {
                done = true;
            }
        }

        List<SpaceModel> path = new List<SpaceModel>();
        PathfindingNode backtrackNode = currentnode;
        while(backtrackNode != null)
        {
            path.Add(backtrackNode.GetSpace());
            backtrackNode = backtrackNode.GetParent();
        }
        path.Reverse();
        foreach (PathfindingNode node in allNodes)
        {
            node.GetSpace().SetNode(null);
        }
        return path;
    }
}