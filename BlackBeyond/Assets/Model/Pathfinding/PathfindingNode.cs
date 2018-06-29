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

    // A* only variable. Guess of remaining distance
    private int ASRemaining;
    private PathfindingNode parent;

    // Create a node on a space.
    public PathfindingNode(SpaceModel space, bool seen, List<SpaceModel> path, int cost)
    {
        thisSpace = space;
        this.seen = seen;
        this.path = path;
        this.cost = cost;
    }

    // Create a node on a space. Special constructor for A* nodes, includes guess of remaining distance
    public PathfindingNode(SpaceModel space, PathfindingNode parent, int cost, SpaceModel destination, bool seen)
    {
        thisSpace = space;
        this.cost = cost;
        this.parent = parent;
        this.seen = seen;

        // Spaces Up/Down (UD) +
        // Spaces Side - (UD) / 2 (Min 0)
        // This section gets the minimum distance between two points on our hex map.
        int vertical = Math.Abs(space.Row - destination.Row);
        int furtherHorizontal = Math.Max(Math.Abs(space.Column - destination.Column) - vertical, 0) / 2;
        ASRemaining = vertical + furtherHorizontal;
    }

    // A* needs a guess of how far this space is from the destination
    public int GetASCost()
    {
        return cost + ASRemaining;
    }
    public PathfindingNode GetParent()
    {
        return parent;
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
            List<SpaceModel> newPath = new List<SpaceModel>(shortestPath)
            {
                thisSpace
            };
            path = new List<SpaceModel>(newPath);
        }
    }

    internal void Update(int newCost, PathfindingNode newParent)
    {
        if (newCost < cost)
        {
            cost = newCost;
            parent = newParent;
        }
    }
}
