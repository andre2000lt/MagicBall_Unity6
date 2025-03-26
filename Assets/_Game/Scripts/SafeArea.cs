using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    private void Awake()
    {
        var myRectTransform = GetComponent<RectTransform>();
        var safeArea = Screen.safeArea;

        var anchorMin = safeArea.min;
        var anchorMax = safeArea.max;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;
        
        myRectTransform.anchorMin = anchorMin;
        myRectTransform.anchorMax = anchorMax;
        

    }
}
