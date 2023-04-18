using System.Collections.Generic;
using UnityEngine;

namespace OneDay.Samples.FallingBlocks
{
    [CreateAssetMenu(fileName = "Falling Block Levels", menuName = "ScriptableObjects/Falling Block Game/ Create Levels",
        order = 1)]
    public class LevelsDefinition : ScriptableObject
    {
        public List<LevelDefinition> Levels;
    }
}