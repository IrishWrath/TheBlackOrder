using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingNode
{
    private int cost;
    private List<Space> path;
    private bool seen;
    private Space thisSpace;

    public PathfindingNode(Space space, bool seen, List<Space> path, int cost)
    {
        thisSpace = space;
        this.seen = seen;
        this.path = path;
        this.cost = cost;
    }

    public bool BeenSeen()
    {
        return seen;
    }
    public void Seen()
    {
        seen = true;
    }

    public Space GetSpace()
    {
        return thisSpace;
    }

    public int GetCost()
    {
        return cost;
    }

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
