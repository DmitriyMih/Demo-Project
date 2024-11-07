using UnityEngine;

namespace GameSystem
{
    [System.Serializable]
    public class TileGizmosManagment
    {
        [SerializeField] private Color color1 = Color.black;
        [SerializeField] private Color color2 = Color.white;
        [SerializeField] private Color heightColor = Color.red;

        [Space]
        [SerializeField] private bool isWireCube;
        [SerializeField] private Vector3 gizmosTileSize = new Vector3(1f, 0.25f, 1f);

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

            Vector3 groupCoverageSize = new Vector3()
            {
                x = gridSettings.GridSize.x * tileSize,
                y = gridSettings.GridSize.y * tileSize,
                z = gridSettings.GridSize.z * tileSize
            };

            Gizmos.color = heightColor;
            Gizmos.DrawWireCube(groupOffset + new Vector3(0f, groupCoverageSize.y / 2f, 0f), groupCoverageSize);

            for (int x = 0; x < gridSettings.GridSize.x; x++)
            {
                for (int z = 0; z < gridSettings.GridSize.z; z++)
                {
                    Vector3 localPosition = groupOffset - gridCenterOffset + new Vector3(x * tileSize, 0, z * tileSize);

                    Gizmos.color = (x + z) % 2 == 0 ? color1 : color2;
                    
                    if (isWireCube)
                        Gizmos.DrawWireCube(localPosition + new Vector3(0f, gizmosTileSize.y * tileSize / 2f, 0f), gizmosTileSize * tileSize);
                    else
                        Gizmos.DrawCube(localPosition + new Vector3(0f, gizmosTileSize.y * tileSize / 2f, 0f), gizmosTileSize * tileSize);
                }
            }
        }
#endif
    }
}