using TowerDefense.Tower;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableTowerUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public TowerData towerData;
    public string towerID => towerData.TowerName;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Transform ogParent;
    private Vector3 ogLocalPosition;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ogParent = transform.parent;
        ogLocalPosition = transform.localPosition;
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        GameObject dropTarget = eventData.pointerEnter;

        if (dropTarget == null ||
            (dropTarget.GetComponentInParent<DeckSlot>() == null &&
             dropTarget.GetComponentInParent<CollectionSlot>() == null))
        {
            ReturnToOriginalPosition();
        }
    }

    public void ReturnToOriginalPosition()
    {
        transform.SetParent(ogParent);
        transform.localPosition = ogLocalPosition;
    }
}