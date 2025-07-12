// PuzzlePiece.cs
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuzzlePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform correctSlot; // Assigned in Inspector
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    private Vector2 originalPosition;
    private bool isPlaced = false;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isPlaced) return;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isPlaced) return;
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isPlaced) return;
        canvasGroup.blocksRaycasts = true;

        if (RectTransformUtility.RectangleContainsScreenPoint(correctSlot, eventData.position, eventData.pressEventCamera))
        {
            rectTransform.anchoredPosition = correctSlot.anchoredPosition;
            isPlaced = true;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            MosaicGameManager.Instance.PiecePlaced();
        }
        else
        {
            rectTransform.anchoredPosition = originalPosition;
        }
    }
}
