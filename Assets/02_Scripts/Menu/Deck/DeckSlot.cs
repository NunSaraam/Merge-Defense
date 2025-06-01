using TowerDefense.Tower;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private TowerType allowedType;
    private DraggableTowerUI assigned;

    public bool HasTower
    {
        get
        {
            if (assigned == null || assigned.gameObject == null)
            {
                assigned = null;
                return false;
            }

            if (assigned.transform.parent != transform)
            {
                assigned = null;
                return false;
            }

            return true;
        }
    }

    public string AssignedID => assigned?.towerID;

    public void OnDrop(PointerEventData eventData)
    {
        var dragged = eventData.pointerDrag?.GetComponent<DraggableTowerUI>();
        if (dragged == null) return;

        if (dragged.towerData.TowerGrade != allowedType)
        {
            Debug.Log($"타워 등급 불일치: {dragged.towerData.TowerGrade} → 허용: {allowedType}");
            dragged.ReturnToOriginalPosition();
            return;
        }

        if (HasTower)
        {
            Debug.Log($"슬롯에 이미 타워가 있음: {assigned.towerID}");
            dragged.ReturnToOriginalPosition();
            return;
        }

        assigned = dragged;
        assigned.transform.SetParent(transform);
        assigned.transform.localPosition = Vector3.zero;

        FindObjectOfType<DeckBuilderManager>()?.CheckDeckValid();

        string a = HasTower ? "할당됨" : "할당되지않음";
        Debug.Log($"{dragged.towerID} : " + a);
    }
}