using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStaticEvent : MonoBehaviour
{
    private void Awake()
    {
        ShootAction.ResetStaticEvent();
        SwordAction.ResetStaticEvent();
        CameraManager.ResetStaticEvent();
        DestructibleCrate.ResetStaticEvent();
        Door.ResetStaticEvent();
        GrenadeProjectile.ResetStaticEvent();
        Unit.ResetStaticEvent();
        BaseAcion.ResetStaticEvent();
        UnitActionSystem.ResetStaticEvent();
        GridSystemVisual.ResetStaticEvent();
    }
}
