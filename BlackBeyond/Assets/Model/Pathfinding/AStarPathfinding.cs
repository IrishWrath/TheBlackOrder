using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityToolbag;

public static class AStarPathfinding
{
    public static List<SpaceModel> GetPathToDestination(SpaceModel startSpace, SpaceModel destSpace)
    {
        List<PathfindingNode> allNodes = new List<PathfindingNode>();

        PathfindingNode currentnode = new PathfindingNode(startSpace, null, 0 , 0, true, destSpace);
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
                        double newPathfindingCost = newNodeCost;
                        if (adjacentSpace.IsHazardous())
                        {
                            newPathfindingCost += 10;
                        }
                        PathfindingNode newNode = new PathfindingNode(adjacentSpace, currentnode, newNodeCost, newPathfindingCost, false, destSpace);
                        allNodes.Add(newNode);
                        adjacentSpace.SetNode(newNode);

                        //Dispatcher.Invoke(() =>
                        //{
                        //    newNode.GetSpace().GetController().SetSelectable((int)newNode.GetRemainingCost());
                        //});
                        //System.Threading.Thread.Sleep(1000);
                    }
                    else
                    {
                        // not null, there is a node here
                        PathfindingNode nextNode = adjacentSpace.GetNode();
                        int newNodeCost = currentnode.GetCost() + adjacentSpace.GetMovementCost();
                        double newPathfindingCost = newNodeCost;
                        if (adjacentSpace.IsHazardous())
                        {
                            newPathfindingCost += 10;
                        }
                        if (!nextNode.BeenSeen())
                        {
                            // Next node hasn't been visited yet
                            nextNode.Update(newNodeCost, newPathfindingCost, currentnode);
                            //Dispatcher.Invoke(() =>
                            //{
                            //    nextNode.GetSpace().GetController().SetSelectable((int)nextNode.GetRemainingCost());

                            //});
                            //System.Threading.Thread.Sleep(1000);
                        }

                    }
                }
            }
            currentnode.Seen();
            //Dispatcher.Invoke(() =>
            //{
            //    currentnode.GetSpace().GetController().SetTest();
            //});
            //System.Threading.Thread.Sleep(5);
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
                            if (node.GetASCost() <= lowestNode.GetASCost())
                            {
                                if (node.GetRemainingCost() <= lowestNode.GetRemainingCost())
                                {
                                    lowestNode = node;
                                }
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

        //Dispatcher.Invoke(() =>
        //{
            foreach (PathfindingNode node in allNodes)
            {
                node.GetSpace().SetNode(null);

                //node.GetSpace().ClearHighlighted();
            }
        //});
        //Dispatcher.Invoke(() =>
        //{
        //    foreach (SpaceModel node in path)
        //    {
        //        node.GetController().SetTestFinal();
        //    }
        //});
        path.Remove(startSpace);
        return path;
    }
}