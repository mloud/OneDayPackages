using UnityEngine;

namespace OneDay.Samples.FallingBlocks
{
    [CreateAssetMenu(fileName = "Falling Block Level", menuName = "ScriptableObjects/Falling Block Game/ Create Level", order = 1)]
    public class LevelDefinition : ScriptableObject
    {
        public int Width;
        public int Height;
        public int[] Table;
        
        public Vector2Int GetCellCoordinatesInWorld(int x, int y)
        {
            x -= Width / 2;
            y = Height - y - 1;
            return new Vector2Int(x, y);
        }
        
        public Vector2Int GetCellCoordinatesInWorld(int index)
        {
            var coordinates = FallingBlockUtils.GetCoordinates(index, Width, Height);
            return GetCellCoordinatesInWorld(coordinates.x, coordinates.y);
        }
    }
}