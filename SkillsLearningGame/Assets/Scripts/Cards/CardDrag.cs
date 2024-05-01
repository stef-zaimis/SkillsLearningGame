using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Logic for the draggable/clickable cards
public class CardDrag : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IInitializePotentialDragHandler
{
    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;

    private CanvasGroup canvasGroup;

    Vector3 offset;

    Transform mTransform;

    private LayoutElement layoutElement;

    // Get necessary components when waking up
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        layoutElement = GetComponent<LayoutElement>();
        mTransform = this.transform;
    }

    // When clicked on
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Card Clicked On");
        offset = transform.position - Input.mousePosition;
    }

    // When you are starting to get dragged
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Card Drag Begun");
        canvasGroup.blocksRaycasts = false;
        layoutElement.ignoreLayout = true;
    }

    // When the drag stops
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Card Dropped");
        canvasGroup.blocksRaycasts = true;
        layoutElement.ignoreLayout = false;
    }

    // During getting dragged
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition + offset;
    }

    // When initializing a potential drag
    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        eventData.useDragThreshold = false;
    }
}