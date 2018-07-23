using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Pathfinding class for players
public static class Pathfinding 
{
    // this uses Dijkstra's Algorithm to get all the spaces a player can visit.
    public static List<PathfindingNode> Dijkstras(SpaceModel startSpace, int maxCost, bool ignoreTerrain)
    {
        List<PathfindingNode> allNodes = new List<PathfindingNode>();
        //List<SpaceModel> shortestPath = new List<SpaceModel>
        //{
        //    startSpace
        //};
        PathfindingNode currentnode = new PathfindingNode(startSpace, null, 0, true, null);

        allNodes.Add(currentnode);

        bool done = false;
        while(!done)
        {
            foreach (SpaceModel adjacentSpace in currentnode.GetSpace().GetAdjacentSpaces())
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
                            if (ignoreTerrain)
                            {
                                nextNode.Update(currentnode.GetCost() + 1, currentnode);
                            }
                            else
                            {
                                nextNode.Update(currentnode.GetCost() + adjacentSpace.GetMovementCost(), currentnode);
                            }
                        }
                    }
                    else
                    {
                        // Is null, need new node
                        int newNodeCost; 

                        if (ignoreTerrain)
                        {
                            newNodeCost = currentnode.GetCost() + 1;
                        }
                        else
                        {
                            newNodeCost = currentnode.GetCost() + adjacentSpace.GetMovementCost();
                        }

                        if (newNodeCost <= maxCost)
                        {
                            PathfindingNode newNode = new PathfindingNode(adjacentSpace, currentnode, newNodeCost, false, null);
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
                //shortestPath.Add(lowestNode.GetSpace());
                currentnode = lowestNode;

            }
        }
        foreach (PathfindingNode node in allNodes)
        {
            node.GetSpace().SetNode(null);
        }

        return allNodes;
    }

    public static List<PathfindingNode> GetSpacesForMovementDijkstras(SpaceModel startSpace, int maxCost)
    {
        return Dijkstras(startSpace, maxCost, false);
    }

    public static List<PathfindingNode> GetFieldOfView(SpaceModel startSpace, int maxDist, MapModel map)
    {
        List<PathfindingNode> nodes = Dijkstras(startSpace, maxDist, true);
        List<PathfindingNode> results = new List<PathfindingNode>();
        foreach(PathfindingNode node in nodes)
        {
            List<SpaceModel> line = MapLinedraw(startSpace, node.GetSpace(), map);
            line.Remove(startSpace);
            line.Remove(node.GetSpace());
            bool blocked = false;
            foreach(SpaceModel space in line)
            {
                blocked |= space.BlocksLOS();
            }
            if(!blocked)
            {
                results.Add(node);
            }
        }
        return results;
    }

    // Helper algorithims
    private static CubeCoord CoordinatesToCubeCoords(SpaceModel space)
    {
        double x = (space.Column - space.Row) / 2;
        double z = space.Row;
        double y = -x - z;
        return new CubeCoord(x, y, z);
    }

    private static DoubledCoord CubeCoordsToCoordinates(CubeCoord coord)
    {
        int col = (int)Math.Round(2 * coord.X + coord.Z);
        int row = (int)Math.Round(coord.Z);
        return new DoubledCoord(col, row);
    }

    private static CubeCoord RoundCubeCoords(CubeCoord cubeCoord)
    {
        double newX = Math.Round(cubeCoord.X);
        double newY = Math.Round(cubeCoord.Y);
        double newZ = Math.Round(cubeCoord.Z);

        double x_diff = Math.Abs(newX - cubeCoord.X);
        double y_diff = Math.Abs(newY - cubeCoord.Y);
        double z_diff = Math.Abs(newZ - cubeCoord.Z);

        if (x_diff > y_diff && x_diff > z_diff)
        {
            newX = -newY - newZ;
        }
        else if (y_diff > z_diff)
        {
            newY = -newX - newZ;
        }
        else
        {
            newZ = -newX - newY;
        }
        return new CubeCoord(newX, newY, newZ);
    }

    private static double Lerp(double a, double b, double t)
    {
        return a + (b - a) * t;
    }

    private static CubeCoord CubeLerp(CubeCoord a, CubeCoord b, double t)
    {
        return new CubeCoord(Lerp(a.X, b.X, t),
                             Lerp(a.Y, b.Y, t),
                             Lerp(a.Z, b.Z, t));
    }

    private static List<SpaceModel> MapLinedraw(SpaceModel start, SpaceModel end, MapModel map)
    {
        CubeCoord a = CoordinatesToCubeCoords(start);
        CubeCoord b = CoordinatesToCubeCoords(end);


        double N = CubeDistance(a, b);
        List<SpaceModel> results = new List<SpaceModel>();
        if (Math.Abs(N) > Double.Epsilon)
        {
            for (int i = 0; i <= N; i++)
            {
                CubeCoord coord = RoundCubeCoords(CubeLerp(a, b, 1 / N * i));
                DoubledCoord normalCoord = CubeCoordsToCoordinates(coord);
                SpaceModel newSpace;
                newSpace = map.GetSpace(normalCoord.Row, normalCoord.Column);
                results.Add(newSpace);
            }
        }
        return results;
    }

    private static double CubeDistance(CubeCoord a, CubeCoord b)
    {
        return (Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y) + Math.Abs(a.Z - b.Z)) / 2;
    }
}

internal class DoubledCoord
{
    public int Row { get; private set; }
    public int Column { get; private set; }

    public DoubledCoord(int col, int row)
    {
        this.Column = col;
        this.Row = row;
    }
}

internal class CubeCoord
{
    public double X { get; private set; }
    public double Y { get; private set; }
    public double Z { get; private set; }

    public CubeCoord(double x, double y, double z)
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
    }
	public override string ToString()
	{
        return X.ToString() + ":" + Y + ":" + Z;
	}
}