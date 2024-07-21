using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GrenadeProjectile : MonoBehaviour
{
    public static event EventHandler onAnyGrenadeExploded;

    [SerializeField] private Transform grenadeExplosionVfxPrefab;
    [SerializeField] private TrailRenderer trailRender;
    [SerializeField] private AnimationCurve arcYAnimationCurve;

    public static void ResetStaticEvent()
    {
        onAnyGrenadeExploded = null;
    }

    private Action onGrenadeBehaviorCompleted;
    private Vector3 targetPosition;
    private float totalDistance;
    private Vector3 positionXZ;

    private void Update()
    {
        Vector3 moveDir = (targetPosition - positionXZ).normalized;

        float moveSpeed = 15f;
        positionXZ += moveDir * moveSpeed * Time.deltaTime;

        float distance = Vector3.Distance(positionXZ, targetPosition);
        float distanceNormalized = 1 - distance / totalDistance;

        float maxHeight = totalDistance / 4f;
        float positionY = arcYAnimationCurve.Evaluate(distanceNormalized) * maxHeight;
        transform.position = new Vector3(positionXZ.x, positionY, positionXZ.z);

        float reachedTargetDistance = 0.2f;
        if (Vector3.Distance(positionXZ, targetPosition) < reachedTargetDistance)
        {
            float damageRadius = 4f;
            Collider[] colliderArray = Physics.OverlapSphere(targetPosition, damageRadius);

            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent<Unit>(out Unit targetUnit))
                {
                    targetUnit.Damage(30);
                }
                if (collider.TryGetComponent<DestructibleCrate>(out DestructibleCrate targetDestructibleCrate))
                {
                    targetDestructibleCrate.Damage();
                }
            }
            onAnyGrenadeExploded?.Invoke(this, EventArgs.Empty);
            trailRender.transform.parent = null;
            Instantiate(grenadeExplosionVfxPrefab, targetPosition + Vector3.up * 1.0f, Quaternion.identity);
            Destroy(gameObject);
            onGrenadeBehaviorCompleted();
        }
    }

    public void SetUp(GridPosition targetGridPosition, Action onGrenadeBehaviorCompleted)
    {
        this.onGrenadeBehaviorCompleted = onGrenadeBehaviorCompleted;
        targetPosition = LevelGrid.Instance.GetWorldPosition(targetGridPosition);
        positionXZ = transform.position;
        positionXZ.y = 0;
        totalDistance = Vector3.Distance(positionXZ, targetPosition);   
    }
}
