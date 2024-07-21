using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Button button;
    [SerializeField] private GameObject selectedGameObject;

    private BaseAcion baseAcion;

    public void SetBaseAction(BaseAcion baseAcion)
    {
        this.baseAcion = baseAcion;
        textMeshPro.text = baseAcion.GetActionName().ToUpper();

        button.onClick.AddListener(() =>
        {
            UnitActionSystem.Instance.SetSelectedAction(baseAcion);
        });
    }
    public void UpdateSelectedVisual()
    {
        BaseAcion selectedBaseAction = UnitActionSystem.Instance.GetSelectedAction();
        if (selectedBaseAction != null)
        {
            selectedGameObject.SetActive(selectedBaseAction == baseAcion);
        }
    }
}
