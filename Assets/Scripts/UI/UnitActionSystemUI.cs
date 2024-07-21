using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainerTransform;
    [SerializeField] private TextMeshProUGUI actionPointText;

    private List<ActionButtonUI> actionButtonsList;

    private void Awake()
    {
        actionButtonsList = new List<ActionButtonUI>();
    }
    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
        UnitActionSystem.Instance.OnSelectedActionChanged += UnitActionSystem_OnSelectedActionChanged;
        UnitActionSystem.Instance.OnActionStarted += UnitActionSystem_OnActionStarted;
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        Unit.OnAnyActionPointChanged += Unit_OnAnyActionPointChanged;
        CreateUnitActionButtons();
        UpdateSelectedVisual();
        UpdateActionPoints();
    }

    private void Unit_OnAnyActionPointChanged(object sender, System.EventArgs e)
    {
        UpdateActionPoints();
    }

    private void TurnSystem_OnTurnChanged(object sender, System.EventArgs e)
    {
        UpdateActionPoints();
    }

    private void UnitActionSystem_OnActionStarted(object sender, System.EventArgs e)
    {
        UpdateActionPoints();
    }

    private void UnitActionSystem_OnSelectedActionChanged(object sender, System.EventArgs e)
    {
        UpdateSelectedVisual();
    }

    private void UnitActionSystem_OnSelectedUnitChanged(object sender, System.EventArgs e)
    {
        if (UnitActionSystem.Instance.GetSelectedUnit() != null) 
        {
            CreateUnitActionButtons();
            UpdateSelectedVisual();
            UpdateActionPoints();
        }
    }

    private void CreateUnitActionButtons()
    {
        //clear button ui before create new
        foreach (Transform buttonTransform in actionButtonContainerTransform)
        {
            Destroy(buttonTransform.gameObject);
        }
        actionButtonsList.Clear();

        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        //create button ui for Unit is being selected;
        foreach (BaseAcion baseAcion in selectedUnit.GetBaseAcionArray())
        {
            Transform actionButtonTransform = Instantiate(actionButtonPrefab, actionButtonContainerTransform);
            ActionButtonUI actionButtonUI = actionButtonTransform.GetComponent<ActionButtonUI>();
            actionButtonUI.SetBaseAction(baseAcion);
            actionButtonsList.Add(actionButtonUI);
        }   
    }

    private void UpdateSelectedVisual()
    {
        if (UnitActionSystem.Instance.GetSelectedUnit() != null)
        {
            foreach (ActionButtonUI actionButtonUI in actionButtonsList)
            {
                actionButtonUI.UpdateSelectedVisual();
            }
        }

    }

    private void UpdateActionPoints()
    {
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        if (selectedUnit != null)
        {
            actionPointText.text = "Action Point: " + selectedUnit.GetActionPoints();
        }
    }

    private void OnDestroy()
    {
        if (UnitActionSystem.Instance != null) {
            UnitActionSystem.Instance.OnSelectedUnitChanged -= UnitActionSystem_OnSelectedUnitChanged;
            UnitActionSystem.Instance.OnSelectedActionChanged -= UnitActionSystem_OnSelectedActionChanged;
            UnitActionSystem.Instance.OnActionStarted -= UnitActionSystem_OnActionStarted;
            TurnSystem.Instance.OnTurnChanged -= TurnSystem_OnTurnChanged;
        }


    }
}
