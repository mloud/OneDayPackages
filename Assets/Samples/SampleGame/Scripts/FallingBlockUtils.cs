using UnityEngine;

namespace OneDay.Samples.FallingBlocks
{
    public static class FallingBlockUtils
    {
        public static int GetIndex(int x, int y, int width)
        {
            return y * width + x;
        }

        public static Vector2Int GetCoordinates(int index, int width, int height)
        {
            return new Vector2Int(index % width, index/width);
        }
        
        public static int GetWorldCoordinatesToLevelIndex(int x, int y, LevelDefinition levelDefinition)
        {
            x += levelDefinition.Width / 2;
            y = levelDefinition.Height - y - 1;
            return y * levelDefinition.Width + x;
        }
     

        public static bool IsWorldCoordinatesInLevel(
            int worldCoordinatesX, int worldCoordinatesY,  LevelDefinition levelDefinition)
        {
            var levelIndex = GetWorldCoordinatesToLevelIndex(worldCoordinatesX, worldCoordinatesX, levelDefinition);
            return levelIndex >= 0 && levelIndex < levelDefinition.Table.Length;
        }
    }
}