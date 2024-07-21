using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridDebugObject : MonoBehaviour
{
    private object gridObject;

    [SerializeField] private TextMeshPro textMeshPro;

    public virtual void SetGridObject(object gridObject)
    { this.gridObject = gridObject; }

    protected virtual void Update()
    {
        textMeshPro.text = gridObject.ToString();
    }
}
