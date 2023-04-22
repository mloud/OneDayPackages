using UnityEngine;

namespace OneDay.Samples.FallingBlocks
{
    public class LevelBuilder : MonoBehaviour
    {
        [SerializeField] private GameObject blockPrefab;
        [SerializeField] private Transform blockContainer;
        public void BuildLevel(LevelDefinition levelDefinition)
        {
            for (int i = 0; i < levelDefinition.Table.Length; i++)
            {
                if (levelDefinition.Table[i] == 1)
                {
                    var coordinates = levelDefinition.GetCellCoordinatesInWorld(i);
                    var blockInstance = Instantiate(blockPrefab, blockContainer);
                    blockInstance.transform.position = new Vector3(coordinates.x, coordinates.y, 0);
                }
            }
        }
    }
}