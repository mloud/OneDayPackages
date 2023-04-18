using System;
using System.Collections.Generic;
using UnityEngine;

namespace OneDay.Samples.FallingBlocks
{
    public class FallingBlock : MonoBehaviour
    {
        public Action<GameObject> StoppedFalling;
        
        [SerializeField] private float fallSpeed;
        [SerializeField] private Rigidbody rb;

        private HashSet<GameObject> TriggeredObjects { get; set; }

        private void Awake() =>
            TriggeredObjects = new HashSet<GameObject>();
         
        public void StartFalling() =>
            rb.isKinematic = false;

        public void TryStopOnPlaceholder()
        {
            var placeholder = GetNearestPlaceholder();
            if (placeholder != null)
            {
                StopFalling();
                transform.position = placeholder.transform.position;
            }
            StoppedFalling(placeholder);
        }

        private GameObject GetNearestPlaceholder()
        {
            if (TriggeredObjects.Count <= 0) return null;

            TriggeredObjects.RemoveWhere(x => x == null);
            GameObject nearestGo = null;
            float nearestDistance = float.MaxValue;
            foreach (var go in TriggeredObjects)
            {
                var sqrDistance = (go.transform.position - transform.position).sqrMagnitude;
                if (sqrDistance < nearestDistance)
                {
                    nearestDistance = sqrDistance;
                    nearestGo = go;
                }
            }
            return nearestGo;
        }
      
        private void StopFalling() =>
            rb.isKinematic = true;

        private void OnCollisionEnter(Collision other)
        {
            if (rb.isKinematic)
                return;
            
            StoppedFalling?.Invoke(GetNearestPlaceholder());
            StopFalling();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (rb.isKinematic)
                return;
            TriggeredObjects.Add(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if (rb.isKinematic)
                return;
            TriggeredObjects.Remove(other.gameObject);
        }
    }
}