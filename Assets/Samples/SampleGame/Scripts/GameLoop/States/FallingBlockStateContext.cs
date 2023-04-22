using System;

namespace OneDay.Samples.FallingBlocks.States
{
    public class FallingBlockStateContext
    {
        // game objects
        public BlockSpawner BlockSpawner;
        public MovingBox MovingBox;
        public FallingBlock FallingBlock;
        public int PlaceholdersCount;
        public int HitsCount;
        public Action LostAction;
        public Action WinAction;
    }
}