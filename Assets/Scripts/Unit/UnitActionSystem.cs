using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    public static void ResetStaticEvent()
    {
        Instance = null;
    } 

    public event EventHandler OnSelectedUnitChanged;
    public event EventHandler OnSelectedActionChanged;
    public event EventHandler<bool> OnBusyChanged;
    public event EventHandler OnActionStarted;

    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;

    private bool isBusy;
    private BaseAcion selectedAction;

    private void Awake()
    {
        if (Instance != null) {
            Debug.LogWarning("There's more than one UnitActionSystem" + transform + "-" + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        SetSelectedUnit(selectedUnit);
    }


    private void Update()
    {
        if (selectedUnit == null)
        {
            return;
        } 
        if (!WaterlaGameManager.Instance.IsGamePlaying())
        {
            return;
        }
        if (isBusy)
        {
            return;
        }
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (!TurnSystem.Instance.IsPlayerTurn())
        {
            return;
        } 
        if (TryHandleUnitSelection()) return;

        HandleSelectedAction();

    }

    private void HandleSelectedAction()
    {
        if (InputManager.Instance.GetLeftMouseButtonDownThisFrame()) {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetMousePosition());
            if (!selectedAction.IsValidActionGridPosition(mouseGridPosition))
            {
                return ;
            }
            if (!selectedUnit.TrySpendActionPointToTakeAction(selectedAction))
            {
                return;
            }
            SetIsBusy();
            selectedAction.TakeAction(mouseGridPosition, ClearIsBusy);
            OnActionStarted?.Invoke(this, EventArgs.Empty);
        }
        
    }

    private void SetIsBusy()
    { 
        isBusy = true; 
        OnBusyChanged?.Invoke(this, isBusy);
    }
    private void ClearIsBusy()
    { 
        isBusy = false;
        OnBusyChanged?.Invoke(this, isBusy);
    }

    private bool TryHandleUnitSelection()
    {
        if (InputManager.Instance.GetLeftMouseButtonDownThisFrame())
        {
            Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.GetMouseScreenPosition());
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))
            {
                if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    if (unit == selectedUnit)
                    {
                        return false;
                    }
                    if (unit.IsEnemy())
                    {
                        //Click on Enemy
                        return false;
                    }
                    SetSelectedUnit(unit);
                    return true;
                }
            }
        }
       
        return false;
    }

    public void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        if (selectedUnit != null) 
        {
            SetSelectedAction(unit.GetAction<MoveAction>());
            OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    public void SetSelectedAction(BaseAcion baseAcion)
    {
        selectedAction = baseAcion;
        OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);
    }
    public Unit GetSelectedUnit()
    { 
        return selectedUnit;
    } 
    public BaseAcion GetSelectedAction()
    {
        return selectedAction;
    }
}
