using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleCrate : MonoBehaviour
{
    public static event EventHandler onAnyDestroyed;

    [SerializeField] private Transform crateDestroyedPrefab;

    public static void ResetStaticEvent()
    {
        onAnyDestroyed = null;
    }

    private GridPosition gridPosition;

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
    }

    public void Damage()
    {
        Transform crateDestroyedTramsform = Instantiate(crateDestroyedPrefab, transform.position, transform.rotation);
        ApplyExplosionToChildren(crateDestroyedTramsform, 150f, transform.position, 10f);
        Destroy(gameObject);
        onAnyDestroyed?.Invoke(this, EventArgs.Empty);
    }
    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }
    private void ApplyExplosionToChildren(Transform root, float explosionForce, Vector3 explosionPosition, float explosionRange)
    {
        foreach (Transform child in root)
        {
            if (child.TryGetComponent<Rigidbody>(out Rigidbody childRigidbody))
            {

                childRigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRange);
                ApplyExplosionToChildren(child, explosionForce, explosionPosition, explosionRange);
            }
        }
    }
}
