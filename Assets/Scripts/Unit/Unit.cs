using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public static event EventHandler OnAnyActionPointChanged;
    public static event EventHandler OnAnyUnitSpawned;
    public static event EventHandler OnAnyUnitDead;

    public static void ResetStaticEvent()
    {
        OnAnyActionPointChanged = null;
        OnAnyUnitSpawned = null;
        OnAnyUnitDead = null;
    }

    [SerializeField] private bool isEnemy;

    private const int ACTION_POINT_MAX = 2;
    private GridPosition gridPosition;

    private BaseAcion[] baseActionArray;
    private HealthSystem healthSystem;
    private int actionPoints = 10;

    private void Awake()
    {
        baseActionArray = GetComponents<BaseAcion>();
        healthSystem = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
        TurnSystem.Instance.OnTurnChanged += TurnSytem_OnTurnChanged;
        healthSystem.OnDead += HealthSystem_OnDead;

        OnAnyUnitSpawned?.Invoke(this, EventArgs.Empty);
    }

    private void HealthSystem_OnDead(object sender, EventArgs e)
    {
        LevelGrid.Instance.RemoveUnitAtGridPosition(gridPosition, this);
        Destroy(gameObject);
        OnAnyUnitDead?.Invoke(this, EventArgs.Empty);
    }

    private void Update()
    {
       

        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != gridPosition)
        {
            GridPosition oldGridPosition = gridPosition;
            gridPosition = newGridPosition;
            LevelGrid.Instance.UnitMovedGridPosition(this, oldGridPosition, newGridPosition);
        }
    }
    public T GetAction<T>() where T: BaseAcion
    {
        foreach (BaseAcion baseAcion in baseActionArray)
        {
            if (baseAcion is T)
            {
                return (T)baseAcion;
            }
        }
        return null;
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }
    public BaseAcion[] GetBaseAcionArray()
    {
        return baseActionArray;
    }

    public bool TrySpendActionPointToTakeAction(BaseAcion baseAcion)
    {
        if (CanSpendActionPointToTakeAction(baseAcion))
        {
            SpendActionPoints(baseAcion.GetActionPointCost());
            return true;
        }
        return false;
    }
    public bool CanSpendActionPointToTakeAction(BaseAcion baseAcion)
    {
        if (actionPoints >= baseAcion.GetActionPointCost())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void SpendActionPoints(int amount)
    {
        actionPoints -= amount;
        OnAnyActionPointChanged?.Invoke(this, EventArgs.Empty);
    }
    public int GetActionPoints()
    {
        return actionPoints;
    }
    private void TurnSytem_OnTurnChanged(object sender, System.EventArgs e)
    {
        if ((IsEnemy() && !TurnSystem.Instance.IsPlayerTurn()) ||
            (!IsEnemy() && TurnSystem.Instance.IsPlayerTurn()))
        {
            actionPoints = ACTION_POINT_MAX;
            OnAnyActionPointChanged?.Invoke(this, EventArgs.Empty);
        }
       
    }
    public bool IsEnemy()
    {
        return isEnemy;
    }
    public void Damage(int damageAmount)
    {
        healthSystem.Damage(damageAmount);
    }
    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }
}
