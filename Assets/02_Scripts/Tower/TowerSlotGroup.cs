using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TowerDefense.Tower
{
    public class TowerSlotGroup : MonoBehaviour
    {
        private List<Transform> slotTransforms = new();

        private void Awake()
        {
            foreach (Transform child in transform)
            {
                slotTransforms.Add(child);
            }
        }

        public Transform GetNearestEmptySlot(Vector3 position)
        {
            return slotTransforms
                .Where(t => t.childCount == 0)
                .OrderBy(t => Vector3.Distance(position, t.position))
                .FirstOrDefault();
        }

        public bool HasSameTowerInSlot(Vector3 position, Tower source, out Tower target, out Transform slot)
        {
            foreach (var s in slotTransforms)
            {
                if (s.childCount == 0) continue;
                var t = s.GetChild(0).GetComponent<Tower>();
                if (t != null && t != source &&
                    t.CurrentType == source.CurrentType &&
                    t.GetCurrentData().TowerName == source.GetCurrentData().TowerName)
                {
                    float dist = Vector3.Distance(position, s.position);
                    if (dist < 0.5f)
                    {
                        target = t;
                        slot = s;
                        return true;
                    }
                }
            }

            target = null;
            slot = null;
            return false;
        }
    }
}