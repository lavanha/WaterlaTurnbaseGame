using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAcion : MonoBehaviour
{
    public static event EventHandler OnAnyActionStarted;
    public static event EventHandler OnAnyActionCompleted;

    public static void ResetStaticEvent()
    {
        OnAnyActionCompleted = null;
        OnAnyActionStarted = null;
    }

    protected Unit unit;
    protected bool IsActive;
    protected Action onActionCompleted;

    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
    }

    public abstract string GetActionName();
    public abstract void TakeAction(GridPosition gridPosition, Action onActionCompleted);

    public virtual bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPosition = GetValidActionGridPositionList();
        return validGridPosition.Contains(gridPosition);
    }

    public abstract List<GridPosition> GetValidActionGridPositionList();
    public virtual int GetActionPointCost()
    {
        return 1;
    }
    protected void ActionStarted(Action onActionCompleted)
    {
        IsActive = true;
        this.onActionCompleted = onActionCompleted;
        OnAnyActionStarted?.Invoke(this, EventArgs.Empty);
    }
    protected void ActionCompleted(Action onActionCompleted)
    {
        IsActive = false;
        onActionCompleted();
        OnAnyActionCompleted?.Invoke(this, EventArgs.Empty);
    }
    public Unit GetUnit()
    {
        return unit;
    }
    public EnemyAIAction GetBestEnemyAIAction()
    {
        List<EnemyAIAction> enemyAIActionList = new List<EnemyAIAction>();
        List<GridPosition> validActionGridPositionList = GetValidActionGridPositionList();

        foreach(GridPosition gridPosition in validActionGridPositionList)
        {
            EnemyAIAction enemyAIAction = GetEnemyAIAction(gridPosition); 
            enemyAIActionList.Add(enemyAIAction);
        }

        if (enemyAIActionList.Count > 0)
        {
            enemyAIActionList.Sort((EnemyAIAction a, EnemyAIAction b) => b.actionValue - a.actionValue);
            return enemyAIActionList[0];
        }
        else
        {   
            return null;
        }

    }
    public abstract EnemyAIAction GetEnemyAIAction(GridPosition gridPosition);
}
