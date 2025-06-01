using TowerDefense.Tower;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private TowerType allowedType;
    private DraggableTowerUI assigned;

    // 실제 하위 오브젝트 존재 여부를 확인하여 HasTower 결정
    public bool HasTower
    {
        get
        {
            // assigned가 null이거나 실제 GameObject가 파괴되었으면 false
            if (assigned == null || assigned.gameObject == null)
            {
                assigned = null; // 참조 정리
                return false;
            }

            // assigned가 실제로 이 슬롯의 자식인지 확인
            if (assigned.transform.parent != transform)
            {
                assigned = null; // 다른 곳으로 이동했으면 참조 해제
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