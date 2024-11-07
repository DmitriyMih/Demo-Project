using UnityEngine;

namespace GameSystem
{
    public static class TilesExtensions
    {

        public static Vector3 CalculateGroupOffset(this Vector3Int gridSize, float tileSize)
        {
            float offsetX = (gridSize.x - 1) * tileSize / 2f;
            float offsetY = (gridSize.z - 1) * tileSize / 2f;

            return new Vector3(offsetX, 0, offsetY);
        }

        public static Vector3 CalculateTotalCenterOffset(this Vector3Int gridSize, Vector2Int groupsCount, Vector2 spacingBetweenGroups, float tileSize)
        {
            float offsetX = (groupsCount.x - 1) * (gridSize.x * tileSize + spacingBetweenGroups.x) / 2f;
            float offsetY = (groupsCount.y - 1) * (gridSize.z * tileSize + spacingBetweenGroups.y) / 2f;

            return new Vector3(offsetX, 0, offsetY);
        }

        public static Vector3 GetGroupOffset(this Vector3Int gridSize, Vector2 spacingBetweenGroups, int gx, int gz, float tileSize)
        {
            return new Vector3() { x = gx * (gridSize.x * tileSize + spacingBetweenGroups.x), z = gz * (gridSize.z * tileSize + spacingBetweenGroups.y) };
        }
    }
}