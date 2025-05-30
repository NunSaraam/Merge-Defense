using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense.Tower
{
    public class TowerDrag : MonoBehaviour
    {
        [SerializeField] private Vector3 offset;
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

            Debug.Log("isMouseDragging");
            offset = transform.position - GetMouseWorldPosition();
            IsDragging = true;
        }

        private void OnMouseDrag()
        {
            if (!IsDragging) return;
            Vector3 newPosition = GetMouseWorldPosition() + offset;
            newPosition.z = transform.position.z;
            transform.position = newPosition;

            var group = FindObjectOfType<TowerSlotGroup>();
            if (group != null)
            {
                Transform nearest = group.GetNearestEmptySlot(transform.position);
                if (nearest != null)
                {
                    group.HighlightSlot(nearest);
                }
            }
        }

        private void OnMouseUp()
        {
            IsDragging = false;

            var group = FindObjectOfType<TowerSlotGroup>();
            if (group == null) return;

            group.HighlightSlot(null);

            if (group.HasSameTowerInSlot(transform.position, currentTower, out Tower match, out Transform matchedSlot))
            {
                if (currentTower.CurrentType < TowerType.Mythical)
                {
                    FindObjectOfType<TowerMergeManager>().Merge(currentTower, match, matchedSlot);
                    return;
                }
            }

            Transform emptySlot = group.GetNearestEmptySlot(transform.position);
            if (emptySlot != null)
            {
                transform.position = emptySlot.position;
                transform.SetParent(emptySlot);
            }
            else
            {
                transform.position = transform.parent.position;
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