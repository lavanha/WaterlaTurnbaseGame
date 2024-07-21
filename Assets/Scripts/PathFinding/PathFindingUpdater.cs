using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingUpdater : MonoBehaviour
{

    private void Start()
    {
        DestructibleCrate.onAnyDestroyed += DestructibleCrate_onAnyDestroyed;
    }

    private void DestructibleCrate_onAnyDestroyed(object sender, System.EventArgs e)
    {
        DestructibleCrate destructibleCrate = sender as DestructibleCrate;
        PathFinding.Instance.SetIsWalkableGridPosition(destructibleCrate.GetGridPosition(), true);
    }
}
