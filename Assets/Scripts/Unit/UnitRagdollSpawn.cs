using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRagdollSpawn : MonoBehaviour
{
    [SerializeField] private Transform unitRagdollPrefab;
    [SerializeField] private Transform originalRootBone;
    
    private HealthSystem healthSystem;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDead += HealthSystem_OnDead;
    }

    private void HealthSystem_OnDead(object sender, System.EventArgs e)
    {
        Transform unitRagdollTransform = Instantiate(unitRagdollPrefab, transform.position, transform.rotation);
        UnitRagdoll unitRagdoll = unitRagdollTransform.GetComponent<UnitRagdoll>();
        unitRagdoll.SetUp(originalRootBone);
    }
}
