using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense.Tower
{
    public class TowerDrag : MonoBehaviour
    {
        private Vector3 offset;
        private Camera mainCamera;
        private Tower currentTower;

        public bool IsDragging { get; private set; } = false;

        private void Awake()
        {
            mainCamera = Camera.main;
            currentTower = GetComponent<Tower>();
        }

        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            offset = transform.position - GetMouseWorldPosition();
            IsDragging = true;
        }

        private void OnMouseDrag()
        {
            if (!IsDragging) return;
            Vector3 newPosition = GetMouseWorldPosition() + offset;
            newPosition.z = transform.position.z;
            transform.position = newPosition;
        }

        private void OnMouseUp()
        {
            IsDragging = false;

            var group = FindObjectOfType<TowerSlotGroup>();
            if (group == null) return;

            if (group.HasSameTowerInSlot(transform.position, currentTower, out Tower other, out Transform targetSlot))
            {
                FindObjectOfType<TowerMergeManager>().Merge(currentTower, other, targetSlot);
                return;
            }

            Transform emptySlot = group.GetNearestEmptySlot(transform.position);
            if (emptySlot != null)
            {
                transform.position = emptySlot.position;
                transform.SetParent(emptySlot);
            }
        }

        private Vector3 GetMouseWorldPosition()
        {
            Vector3 mouseScreen = Input.mousePosition;
            mouseScreen.z = mainCamera.WorldToScreenPoint(transform.position).z;
            return mainCamera.ScreenToWorldPoint(mouseScreen);
        }
    }
}