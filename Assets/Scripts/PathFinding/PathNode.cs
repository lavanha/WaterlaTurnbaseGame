using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private GridPosition gridPosition;
    private int gCost;
    private int fCost;
    private int hCost;
    private PathNode cameFromPathNode;
    private bool isWalkable = true;

    public PathNode(GridPosition gridPosition)
    {
        this.gridPosition = gridPosition;
    }
    public override string ToString()
    {
        return gridPosition.ToString();
    }
    public int GetGCost()
    {
        return gCost;
    }
    public int GetFCost()
    {
        return fCost;
    }
    public int GetHCost()
    {
        return hCost;
    }
    public void SetGCost(int gCost)
    {
        this.gCost = gCost;
    }
    public void SetHCost(int hCost)
    {
        this.hCost = hCost; 
    }
    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
    public void ResetCamePathNode()
    {
        cameFromPathNode = null;
    }
    public void SetCamPathNode(PathNode pathNode)
    {
        cameFromPathNode = pathNode;
    }
    public PathNode GetCamePathNode()
    {
        return cameFromPathNode;
    }
    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }
    public bool IsWalkable()
    {
        return isWalkable;
    }
    public void SetIsWalkable(bool isWalkable)
    {
        this.isWalkable = isWalkable;
    }
}
