using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject actionCameraGameObject;

    public static event EventHandler ShootCameraVisualCompleted;

    public static void ResetStaticEvent()
    {
        ShootCameraVisualCompleted = null;
    }

    private void Start()
    {
        BaseAcion.OnAnyActionStarted += BaseAcion_OnAnyActionStarted;
        BaseAcion.OnAnyActionCompleted += BaseAcion_OnAnyActionCompleted;
        HideActionCameraVirtual();
    }

    private void BaseAcion_OnAnyActionCompleted(object sender, System.EventArgs e)
    {
        switch (sender) 
        {
            case ShootAction:
                HideActionCameraVirtual();
                break;
        }
    }

    private void BaseAcion_OnAnyActionStarted(object sender, System.EventArgs e)
    {
        switch (sender)
        {
            case ShootAction shootAction:
                Unit shooterUnit = shootAction.GetUnit();
                Unit targetUnit = shootAction.GetTargetUnit();

                Vector3 cameraCharacterHeight = Vector3.up * 1.7f;
                Vector3 shootDir = (targetUnit.GetWorldPosition() - shooterUnit.GetWorldPosition()).normalized;

                float shoulderOffsetAmount = 0.5f;
                Vector3 shoulderOffset = Quaternion.Euler(0,90,0) * shootDir * shoulderOffsetAmount;
                Vector3 actionCameraPosition = shooterUnit.GetWorldPosition() + cameraCharacterHeight + shoulderOffset + (shootDir * -1);
                actionCameraGameObject.transform.position = actionCameraPosition;
                actionCameraGameObject.transform.LookAt(targetUnit.GetWorldPosition() + cameraCharacterHeight);
                ShowActionCameraVirtual();
                break;
        }
    }

    private void ShowActionCameraVirtual()
    {
        actionCameraGameObject.SetActive(true);
        ShootCameraVisualCompleted?.Invoke(this, EventArgs.Empty);
    }
    private void HideActionCameraVirtual()
    {
        actionCameraGameObject.SetActive(false);
    }
}
