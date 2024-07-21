using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallBackLoader : MonoBehaviour
{
    private bool isFirstCallBackLoader = true;

    private void Update()
    {
        if (isFirstCallBackLoader)
        {
            isFirstCallBackLoader = false;
            Loader.CallBackLoaderScene();
        }
    }
}
