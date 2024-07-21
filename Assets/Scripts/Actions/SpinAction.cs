using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpinAction : BaseAcion
{
    private float totalSpinAmount;

    private void Update()
    {
        if (!IsActive)
        {
            return;
        }

        float spinAddAmount = 360f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);
        totalSpinAmount += spinAddAmount;

        if (totalSpinAmount >= 360f)
        {
            ActionCompleted(onActionCompleted);
        }
        
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionCompleted)
    {
        totalSpinAmount = 0f;
        ActionStarted(onActionCompleted);
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        GridPosition unitGridPosition = unit.GetGridPosition();
        return new List<GridPosition>
        {
            unitGridPosition
        };
    }

    public override string GetActionName()
    {
        return "SPIN";
    }
    public override int GetActionPointCost()
    {
        return 2;
    }
    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        return new EnemyAIAction
        {
            gridPosition = gridPosition,
            actionValue = 0
        };
    }
}
