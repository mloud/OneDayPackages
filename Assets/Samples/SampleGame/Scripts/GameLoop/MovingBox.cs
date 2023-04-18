using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace OneDay.Samples.FallingBlocks
{
    public class MovingBox : MonoBehaviour
    {
        public Vector3 Direction { get; private set; }
        
        [SerializeField] private Transform rightTransform;
        [SerializeField] private Transform leftTransform;
        [SerializeField] private float speed;

        private Coroutine MovingCoroutine { get; set; }
      
        public void StartMoving()
        {
            StopMoving();
            MovingCoroutine = StartCoroutine(MoveAsync());
        }

        public void StopMoving()
        {
            if (MovingCoroutine != null)
            {
                StopCoroutine(MovingCoroutine);
                MovingCoroutine = null;
            }
        }
        
        private IEnumerator MoveAsync()
        {
            while (true)
            {
                Direction = Vector3.right;
                yield return transform.DOMove(rightTransform.position,
                        (rightTransform.position - transform.position).magnitude / speed)
                    .SetEase(Ease.Linear).WaitForCompletion();
                Direction = Vector3.left;
                yield return transform.DOMove(leftTransform.position,
                        (leftTransform.position - transform.position).magnitude / speed)
                    .SetEase(Ease.Linear).WaitForCompletion();
            }
        }
    }
}
