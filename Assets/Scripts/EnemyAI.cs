using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private float timer;
    private enum State
    {
        WaitingForEnermyTurn,
        TakingTurn,
        Busy
    }
    private State state;

    private void Awake()
    {
        state = State.WaitingForEnermyTurn;
    }

    private void Start()
    {
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
    }

    private void TurnSystem_OnTurnChanged(object sender, System.EventArgs e)
    {
        if (!TurnSystem.Instance.IsPlayerTurn())
        {
            state = State.TakingTurn;
            timer = 2f;
        }
    }

    private void Update()
    {
        if (TurnSystem.Instance.IsPlayerTurn())
        {
            return;
        }
        switch (state)
        {
            case State.WaitingForEnermyTurn:
                break;
            case State.TakingTurn:
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    if (TryTakeEnemyAIAction(SetStateTakingTurn))
                    {
                        state = State.Busy;
                    }
                    else
                    {
                        TurnSystem.Instance.NextTurn();
                    }
          
                }
                break;
            case State.Busy:
                break;
        }
        
    }
    private void SetStateTakingTurn()
    {
        timer = 0.5f;
        state = State.TakingTurn;
    }
    private bool TryTakeEnemyAIAction(Action onEnemyAIActionCompleted)
    {
        foreach (Unit enemyUnit in UnitManager.Instance.GetEnemyUnitList())
        {
            if (TryTakeEnemyAIAction(enemyUnit, onEnemyAIActionCompleted))
            {
                return true;
            }

        }
        return false;
    }
    private bool TryTakeEnemyAIAction(Unit enemyUnit, Action onEnemyAIActionCompleted)
    {
        EnemyAIAction bestEnemyAIAction = null;
        BaseAcion bestBaseAction = null;    
        foreach(BaseAcion baseAcion in enemyUnit.GetBaseAcionArray())
        {
            if (!enemyUnit.CanSpendActionPointToTakeAction(baseAcion))
            {
                //Enemy Can not afford this action
                continue;
            }
            if (bestBaseAction == null)
            {
                bestBaseAction = baseAcion;
                bestEnemyAIAction = baseAcion.GetBestEnemyAIAction();
            }
            else
            {
                EnemyAIAction testEnemyAIAction = baseAcion.GetBestEnemyAIAction();
                if (testEnemyAIAction != null && testEnemyAIAction.actionValue > bestEnemyAIAction.actionValue)
                {
                    bestEnemyAIAction = testEnemyAIAction;
                    bestBaseAction = baseAcion;
                } 
            }
        }

        if (bestEnemyAIAction != null && enemyUnit.TrySpendActionPointToTakeAction(bestBaseAction))
        {
            bestBaseAction.TakeAction(bestEnemyAIAction.gridPosition, onEnemyAIActionCompleted);
            return true;
        }
        else
        {
            return false;
        }
    }
}
