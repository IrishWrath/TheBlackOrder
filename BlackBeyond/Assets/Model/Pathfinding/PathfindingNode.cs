using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A pathfinding node. TODO might need updates
public class PathfindingNode
{
    private int cost;
    private List<Space> path;
    private bool seen;
    private Space thisSpace;

    // Create a node on a space.
    public PathfindingNode(Space space, bool seen, List<Space> path, int cost)
    {
        thisSpace = space;
        this.seen = seen;
        this.path = path;
        this.cost = cost;
    }

    // has this node been visited by the pathfinder
    public bool BeenSeen()
    {
        return seen;
    }

    // This node has been visited
    public void Seen()
    {
        seen = true;
    }

    // Gets the space this node is attached to.
    public Space GetSpace()
    {
        return thisSpace;
    }

    // Gets the cost to travel to this node.
    public int GetCost()
    {
        return cost;
    }

    // Is this new path better than the current one? If yes, replace the current one.
    internal void Update(int newCost, List<Space> shortestPath)
    {
        if(newCost < cost)
        {
            cost = newCost;
            List<Space> newPath = new List<Space>(shortestPath);
            newPath.Add(thisSpace);
            path = new List<Space>(newPath);
        }
    }
}
