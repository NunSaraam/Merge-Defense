using UnityEngine;
using UnityEngine.EventSystems;

public class CollectionSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        var dragged = eventData.pointerDrag?.GetComponent<DraggableTowerUI>();
        if (dragged == null) return;

        dragged.transform.SetParent(transform);
        dragged.transform.localPosition = Vector3.zero;
    }
}
