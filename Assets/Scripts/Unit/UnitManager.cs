using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance { get; private set; }

    private List<Unit> unitList;
    private List<Unit> enemyUnitList;
    private List<Unit> friendlyUnitList;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("There's more than one UnitActionSystem" + transform + "-" + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;

        unitList = new List<Unit>();
        enemyUnitList = new List<Unit>();
        friendlyUnitList = new List<Unit>();
    }
    private void Start()
    {
        Unit.OnAnyUnitSpawned += Unit_OnAnyUnitSpawned;
        Unit.OnAnyUnitDead += Unit_OnAnyUnitDead;
    }

    private void Unit_OnAnyUnitDead(object sender, System.EventArgs e)
    {
        Unit unit = sender as Unit;
        unitList.Remove(unit);
        if (unit.IsEnemy())
        {
            enemyUnitList.Remove(unit);
        }
        else
        {
            friendlyUnitList.Remove(unit);
            if (friendlyUnitList.Count > 0)
            {
                UnitActionSystem.Instance.SetSelectedUnit(friendlyUnitList[0]);
            }
            else
            {
                UnitActionSystem.Instance.SetSelectedUnit(null);
            }
        }
    }

    private void Unit_OnAnyUnitSpawned(object sender, System.EventArgs e)
    {
        Unit unit = sender as Unit;
        unitList.Add(unit);
        if (unit.IsEnemy())
        {
            enemyUnitList.Add(unit);
        }
        else
        {
            friendlyUnitList.Add(unit);
        }
    }

    public List<Unit> GetEnemyUnitList()
    {
        return enemyUnitList;
    }
    public List <Unit> GetFriendUnitList()
    {
        return friendlyUnitList;
    }
    public List<Unit> GetUnitList()
    {
        return unitList;
    }
}
