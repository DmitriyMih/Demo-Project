using UnityEngine;

namespace GameSystem
{
    [System.Serializable]
    public class TileGizmosManagment
    {
#if UNITY_EDITOR
        public void OnGizmos(ITileGrid gridSettings)
        {
            float tileSize = PlaceManagment.PlaceTileSize;
            Gizmos.matrix = gridSettings.GetTransform().localToWorldMatrix;

            Vector3 totalCenterOffset = gridSettings.GridSize.CalculateTotalCenterOffset(gridSettings.GroupsCount, gridSettings.SpacingBetweenGroups, tileSize);

            for (int gx = 0; gx < gridSettings.GroupsCount.x; gx++)
            {
                for (int gy = 0; gy < gridSettings.GroupsCount.y; gy++)
                {
                    Vector3 groupOffset = gridSettings.GridSize.GetGroupOffset(gridSettings.SpacingBetweenGroups, gx, gy, tileSize) - totalCenterOffset;
                    DrawGrid(gridSettings, groupOffset, tileSize);
                }
            }

            Gizmos.matrix = Matrix4x4.identity;
        }

        private void DrawGrid(ITileGrid gridSettings, Vector3 groupOffset, float tileSize)
        {
            Vector3 gridCenterOffset = gridSettings.GridSize.CalculateGroupOffset(tileSize);

            for (int x = 0; x < gridSettings.GridSize.x; x++)
            {
                for (int z = 0; z < gridSettings.GridSize.z; z++)
                {
                    Vector3 localPosition = groupOffset - gridCenterOffset + new Vector3(x * tileSize, 0, z * tileSize);

                    Gizmos.color = (x + z) % 2 == 0 ? Color.black : Color.white;
                    Gizmos.DrawCube(localPosition, Vector3.one * tileSize);
                }
            }
        }
#endif
    }
}