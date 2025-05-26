using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense.Tower
{
    public class TowerDrag : MonoBehaviour
    {
        private Vector3 offset;
        private Camera mainCamera;
        private bool isDragging = false;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            offset = transform.position - GetMouseWorldPosition();
            isDragging = true;
        }

        private void OnMouseDrag()
        {
            if (!isDragging) return;
            Vector3 newPosition = GetMouseWorldPosition() + offset;
            newPosition.z = transform.position.z;
            transform.position = newPosition;
        }

        private void OnMouseUp()
        {
            isDragging = false;

            Collider2D[] hits = Physics2D.OverlapPointAll(transform.position);
            foreach (var hit in hits)
            {
                if (hit.gameObject != gameObject && hit.TryGetComponent(out Tower otherTower))
                {
                    if (TryGetComponent(out Tower thisTower) &&
                        FindObjectOfType<TowerMerge>().TryMerge(thisTower, otherTower))
                    {
                        return;
                    }
                }
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