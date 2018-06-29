using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A pathfinding node. TODO might need updates
public class PathfindingNode
{
    private int cost;
    private List<SpaceModel> path;
    private bool seen;
    private SpaceModel thisSpace;

    // Create a node on a space.
    public PathfindingNode(SpaceModel space, bool seen, List<SpaceModel> path, int cost)
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
    public SpaceModel GetSpace()
    {
        return thisSpace;
    }

    // Gets the cost to travel to this node.
    public int GetCost()
    {
        return cost;
    }

    public List<SpaceModel> GetPath()
    {
        return path;
    }

    // Is this new path better than the current one? If yes, replace the current one.
    internal void Update(int newCost, List<SpaceModel> shortestPath)
    {
        if(newCost < cost)
        {
            cost = newCost;
            List<SpaceModel> newPath = new List<SpaceModel>(shortestPath);
            newPath.Add(thisSpace);
            path = new List<SpaceModel>(newPath);
        }
    }
}
