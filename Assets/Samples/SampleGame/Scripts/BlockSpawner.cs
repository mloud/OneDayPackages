using UnityEngine;

namespace OneDay.Samples.FallingBlocks
{
    public class BlockSpawner : MonoBehaviour
    {
        [SerializeField] private FallingBlock fallingBlockPrefab;
        public FallingBlock Spawn(Vector3 speed)
        {
            var copy = Instantiate(fallingBlockPrefab);
            var sourcePos = transform.position;
            sourcePos.x = Mathf.Round(sourcePos.x);
            copy.transform.position = sourcePos + new Vector3(0, -1.1f, 0);
            
            copy.StartFalling();
            return copy;
        }
    }
}