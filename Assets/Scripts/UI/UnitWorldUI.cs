using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitWorldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI actionPointText;
    [SerializeField] Unit unit;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private HealthSystem healthSystem;

    private void Start()
    {
        Unit.OnAnyActionPointChanged += Unit_OnAnyActionPointChanged;
        healthSystem.OnDamage += HealthSystem_OnDamage;
        UpdateActionPointText();
        UpdateHealthBar();
    }

    private void HealthSystem_OnDamage(object sender, System.EventArgs e)
    {
        UpdateHealthBar();
    }

    private void Unit_OnAnyActionPointChanged(object sender, System.EventArgs e)
    {
        UpdateActionPointText();
    }

    private void UpdateActionPointText()
    {
        actionPointText.text = unit.GetActionPoints().ToString();
    }
    private void UpdateHealthBar()
    {
        healthBarImage.fillAmount = healthSystem.GetHealthNormalized();
    }
}
