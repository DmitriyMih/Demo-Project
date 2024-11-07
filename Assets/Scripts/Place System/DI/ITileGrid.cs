using UnityEngine;

namespace GameSystem
{
    public interface ITileGrid
    {
        public Vector3Int GridSize { get; }
        public Vector2Int GroupsCount { get; }
        public Vector2 SpacingBetweenGroups { get; }

        public abstract Transform GetTransform();
    }
}