using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisualSingle : MonoBehaviour
{
    [SerializeField] private MeshRenderer gridSystemVisualSingle;

    public void Show(Material material)
    {
        gameObject.SetActive(true);
        gridSystemVisualSingle.material = material;
    }
    public void Hide() { 
        gameObject.SetActive(false);
    }
}
