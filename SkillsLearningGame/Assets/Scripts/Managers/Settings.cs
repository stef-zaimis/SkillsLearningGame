using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// This script is not needed anymore and will likely get removed
public static class Settings
{
    public static GameManager gameManager;

    private static ResourceManager _resourceManager;

    public static ResourceManager GetResourceManager()
    {
        if (_resourceManager == null)
        {
            _resourceManager = Resources.Load("ResourceManager") as ResourceManager;
        }
        return _resourceManager;
    }
    public static List<RaycastResult> GetUIObjects()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, raycastResults);
        return raycastResults;
    }

}

