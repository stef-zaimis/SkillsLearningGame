using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorDefault;
    [SerializeField] private Texture2D cursorHover;
    [SerializeField] private Texture2D cursorClick;
    private Texture2D currentCursor;

    private bool isCursorOverClickable;

    void Start()
    {
        SetCursor(cursorDefault);
    }

    void Update()
    {
        UpdateCursor();
        HandleMouseInteraction();
    }

    private void SetCursor(Texture2D cursorTexture)
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        currentCursor = cursorTexture;
    }

    private void UpdateCursor()
    {
        // Check if the mouse is over a UI element that can be clicked
        if (EventSystem.current.IsPointerOverGameObject())
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            isCursorOverClickable = false;
            foreach (var result in results)
            {
                if (result.gameObject.GetComponent<IPointerClickHandler>() != null)
                {
                    if (currentCursor != cursorHover)
                    {
                        SetCursor(cursorHover);
                    }
                    isCursorOverClickable = true;
                    return; // Exit early since we found a clickable object
                }
            }
        }

        if (!isCursorOverClickable && currentCursor == cursorHover)
        {
            SetCursor(cursorDefault);
        }
    }

    private void HandleMouseInteraction()
    {
        if (Input.GetMouseButtonDown(0) && !isCursorOverClickable)
        {
            SetCursor(cursorClick);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (isCursorOverClickable && currentCursor != cursorHover)
            {
                SetCursor(cursorHover);
            }
            else if (!isCursorOverClickable)
            {
                SetCursor(cursorDefault);
            }
        }
    }
}